﻿using MongoDB.Driver;
using Restaurant.Interfaces;

namespace Restaurant.Services;

public class LogServices : ILogServices
{
    private readonly IMongoCollection<ErrorLog> _collection;

    public LogServices(IMongoCollection<ErrorLog> collection)
    {
        _collection = collection;
    }


    public async Task CreateLog(ErrorLog log)
    {
        await _collection.InsertOneAsync(log);
    }




}
