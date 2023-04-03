﻿using System;
using System.IO;
using System.Net.WebSockets;
using CryptoTools.Data;
using Microsoft.EntityFrameworkCore;

namespace CryptoTools;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public static Action? ShowLogin { get; set; }
    public static Action? ShowApp { get; set; }

    private App()
    {
        InitializeComponent();
        using var db = new CryptoDbContext();
        var directory = Path.GetDirectoryName(CryptoDbContext.DbPath);
        if (directory is not null) Directory.CreateDirectory(directory);
        // Only in Development, remove in Production
        if (File.Exists(CryptoDbContext.DbPath)) File.Delete(CryptoDbContext.DbPath);
        db.Database.EnsureCreated();
        db.Database.Migrate();
        db.Seed();
    }

    public static ClientWebSocket ClientWebSocket { get; set; } = new();
    public static int UserId { get; set; } = 0;

    public static string UserName { get; set; } = string.Empty;
}