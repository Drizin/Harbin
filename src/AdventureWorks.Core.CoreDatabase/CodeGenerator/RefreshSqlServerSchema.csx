﻿/// <summary>
/// This CSX Script will invoke SqlServerSchemaReader, which extracts the schema of SQL database and saves into a JSON file.
/// The easiest way to launch csi.exe (which is shipped with Visual Studio) to run this script is by using PowerShell script RefreshDatabaseSchema.ps1
/// You can do that from Visual Studio (see instructions in RefreshDatabaseSchema.ps1) or you can just execute "Powershell RefreshDatabaseSchema.ps1"
/// </summary>

// System libraries
#r "System.Data.dll"

// Load third-party libraries by their relative paths, relative to "$Env:userprofile\.nuget\packages\"
#r "dapper\2.0.35\lib\netstandard2.0\Dapper.dll"
#r "newtonsoft.json\12.0.3\lib\netstandard2.0\Newtonsoft.Json.dll"
#r "codegencs.dbschema\1.0.0\lib\netstandard2.0\CodegenCS.DbSchema.dll"

#load "0-EDIT-Variables.csx"

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Data;
using System.Data.SqlClient;
using CodegenCS.DbSchema.SqlServer;

// Helpers to get the location of the current CSX script
public static string GetScriptPath([CallerFilePath] string path = null) => path;
public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);


// location relative to the CSX script
string outputJsonSchema = Path.GetFullPath(Path.Combine(GetScriptFolder(), jsonSchemaPath)); 

Func<IDbConnection> connectionFactory = () => new SqlConnection(connectionString);
var reader = new SqlServerSchemaReader(connectionFactory);
reader.ExportSchemaToJSON(outputJsonSchema);
