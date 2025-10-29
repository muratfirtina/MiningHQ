using System.Globalization;

namespace Application.Utilities;

/// <summary>
/// UTM (Universal Transverse Mercator) koordinatlarını WGS84 (GPS) koordinatlarına dönüştürür
/// Zone 35T (Türkiye) için optimize edilmiştir
/// </summary>
public static class CoordinateConverter
{
    private const double a = 6378137.0; // WGS84 elipsoid yarı-majör ekseni (metre)
    private const double e = 0.081819191; // WGS84 eksantriklik
    private const double e1sq = 0.006739497; // e'^2
    private const double k0 = 0.9996; // UTM ölçek faktörü
    
    /// <summary>
    /// UTM Zone 35T koordinatlarını WGS84 Latitude/Longitude'a çevirir
    /// </summary>
    /// <param name="utmEasting">UTM Easting (X) - Doğruluk değeri</param>
    /// <param name="utmNorthing">UTM Northing (Y) - Kuzeyleme değeri</param>
    /// <returns>Tuple(Latitude, Longitude) derece cinsinden</returns>
    public static (double Latitude, double Longitude) UtmToWgs84(double utmEasting, double utmNorthing)
    {
        // Zone 35 için merkezi meridyen (derece cinsinden)
        int zone = 35;
        double centralMeridianDegrees = (zone - 1) * 6 - 180 + 3; // Zone 35 için: 27 derece
        
        // False Easting çıkar
        double x = utmEasting - 500000.0;
        double y = utmNorthing;
        
        // M hesapla (meridyen ark uzunluğu)
        double M = y / k0;
        double mu = M / (a * (1 - e * e / 4 - 3 * e * e * e * e / 64 - 5 * e * e * e * e * e * e / 256));
        
        double e1 = (1 - Math.Sqrt(1 - e * e)) / (1 + Math.Sqrt(1 - e * e));
        double J1 = (3 * e1 / 2 - 27 * e1 * e1 * e1 / 32);
        double J2 = (21 * e1 * e1 / 16 - 55 * e1 * e1 * e1 * e1 / 32);
        double J3 = (151 * e1 * e1 * e1 / 96);
        double J4 = (1097 * e1 * e1 * e1 * e1 / 512);
        
        // Footpoint latitude (radyan cinsinden)
        double fp = mu + J1 * Math.Sin(2 * mu) + J2 * Math.Sin(4 * mu) + J3 * Math.Sin(6 * mu) + J4 * Math.Sin(8 * mu);
        
        double C1 = e1sq * Math.Cos(fp) * Math.Cos(fp);
        double T1 = Math.Tan(fp) * Math.Tan(fp);
        double R1 = a * (1 - e * e) / Math.Pow(1 - e * e * Math.Sin(fp) * Math.Sin(fp), 1.5);
        double N1 = a / Math.Sqrt(1 - e * e * Math.Sin(fp) * Math.Sin(fp));
        double D = x / (N1 * k0);
        
        // Latitude hesaplama (radyan cinsinden)
        double Q1 = N1 * Math.Tan(fp) / R1;
        double Q2 = (D * D / 2);
        double Q3 = (5 + 3 * T1 + 10 * C1 - 4 * C1 * C1 - 9 * e1sq) * D * D * D * D / 24;
        double Q4 = (61 + 90 * T1 + 298 * C1 + 45 * T1 * T1 - 3 * C1 * C1 - 252 * e1sq) * D * D * D * D * D * D / 720;
        double latitudeRadians = fp - Q1 * (Q2 - Q3 + Q4);
        
        // Longitude hesaplama (radyan cinsinden offset)
        double Q5 = D;
        double Q6 = (1 + 2 * T1 + C1) * D * D * D / 6;
        double Q7 = (5 - 2 * C1 + 28 * T1 - 3 * C1 * C1 + 8 * e1sq + 24 * T1 * T1) * D * D * D * D * D / 120;
        double longitudeOffset = (Q5 - Q6 + Q7) / Math.Cos(fp); // Radyan cinsinden offset
        
        // CRITICAL FIX: Longitude offset'i dereceye çevir, sonra central meridian ekle
        double longitudeOffsetDegrees = longitudeOffset * 180.0 / Math.PI;
        double longitudeDegrees = centralMeridianDegrees + longitudeOffsetDegrees;
        
        // Latitude'ü dereceye çevir
        double latitudeDegrees = latitudeRadians * 180.0 / Math.PI;
        
        return (latitudeDegrees, longitudeDegrees);
    }
    
    /// <summary>
    /// UTM koordinatlarının geçerli olup olmadığını kontrol eder
    /// </summary>
    public static bool IsValidUtm(double? utmEasting, double? utmNorthing)
    {
        if (!utmEasting.HasValue || !utmNorthing.HasValue)
            return false;
            
        // Zone 35T için tipik değer aralıkları (Türkiye)
        // Easting: 166,000 - 833,000 metre
        // Northing: 0 - 9,329,000 metre (kuzey yarımküre)
        return utmEasting.Value >= 166000 && utmEasting.Value <= 833000 &&
               utmNorthing.Value >= 0 && utmNorthing.Value <= 9329000;
    }
    
    /// <summary>
    /// UTM koordinatlarını güvenli bir şekilde WGS84'e çevirir
    /// Geçersiz koordinatlar için null döner
    /// </summary>
    public static (double? Latitude, double? Longitude) SafeUtmToWgs84(double? utmEasting, double? utmNorthing)
    {
        Console.WriteLine(string.Format(CultureInfo.InvariantCulture, 
            "[CoordinateConverter] SafeUtmToWgs84 called with Easting={0}, Northing={1}", 
            utmEasting, utmNorthing));
        
        if (!IsValidUtm(utmEasting, utmNorthing))
        {
            Console.WriteLine("[CoordinateConverter] Invalid UTM coordinates!");
            return (null, null);
        }
            
        try
        {
            var (lat, lon) = UtmToWgs84(utmEasting!.Value, utmNorthing!.Value);
            
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, 
                "[CoordinateConverter] Converted to Lat={0:F6}, Lon={1:F6}", 
                lat, lon));
            
            // Sonuç kontrol (Türkiye yaklaşık: 36-42 Lat, 26-45 Lon)
            if (lat < 36 || lat > 42 || lon < 26 || lon > 45)
            {
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                    "[CoordinateConverter] Coordinates out of Turkey bounds! Lat={0:F6}, Lon={1:F6}", 
                    lat, lon));
                return (null, null);
            }
                
            return (lat, lon);
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[CoordinateConverter] Exception: {0}", ex.Message));
            return (null, null);
        }
    }
}
