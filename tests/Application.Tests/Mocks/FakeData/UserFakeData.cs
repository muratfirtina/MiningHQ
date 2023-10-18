using Core.Security.Entities;
using Core.Test.Application.FakeData;
using System;
using System.Collections.Generic;

namespace Application.Tests.Mocks.FakeData;

public class UserFakeData : BaseFakeData<User, Guid>
{
    public override List<User> CreateFakeData()
    {
        Guid id = Guid.Empty;
        List<User> data =
            new()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Denekadı1",
                    LastName = "Denek1soyadı",
                    Email = "denek1@email.com",
                    PasswordHash = new byte[] { },
                    PasswordSalt = new byte[] { },
                    Status = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Denekadı2",
                    LastName = "Denek2soyadı",
                    Email = "denek2@email.com",
                    PasswordHash = new byte[] { },
                    PasswordSalt = new byte[] { },
                    Status = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };
        return data;
    }
}
