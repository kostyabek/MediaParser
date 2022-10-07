﻿namespace Domain.Common.Options;

public class DatabaseOptions
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string MediaCollectionName { get; set; }
}