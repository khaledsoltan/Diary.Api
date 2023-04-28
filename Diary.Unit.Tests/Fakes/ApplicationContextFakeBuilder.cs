using Microsoft.EntityFrameworkCore;
using System;
using Api.Host.Domain.Entites;
using Repository.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Diary.Unit.Tests.Fakes;

public class ApplicationContextFakeBuilder : IDisposable
{
    
    private readonly RepositoryContext _context;
    
    
    private EntityEntry<DiarY> _basediary;
    
    // Generate sample object for diary.
    public ApplicationContextFakeBuilder WithDiary()
    {
        _basediary = _context.Add(new DiarY { UserId = "", DiaryName = "Diary Day NO" });
        return this;
    }
  

    
    
    
    
    
    public RepositoryContext Build()
    {
        _context.SaveChanges();
        return _context;
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
    
}