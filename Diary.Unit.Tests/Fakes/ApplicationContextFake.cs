using Microsoft.EntityFrameworkCore;
using System;
using Repository.Context;


namespace TestProject1.Fakes;


public class ApplicationContextFake : RepositoryContext
{
    public ApplicationContextFake() : base(new DbContextOptionsBuilder<RepositoryContext>()
        .UseInMemoryDatabase(databaseName: $"DiaryTest-{Guid.NewGuid()}").Options) {}

}