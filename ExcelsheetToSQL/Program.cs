// See https://aka.ms/new-console-template for more information

using System;
using System.Data.SqlClient;
using System.IO;
using ExcelDataReader;
using System.Text;
using ExcelsheetToSQL;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AppContext = ExcelsheetToSQL.AppContext;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var inFilePath = "D:\\After GRAD\\Freelance\\Dumb data.xlsx";
var outFilePath = "D:\\After GRAD\\Freelance\\file.json";
List<workers> workersList = new List<workers>();

using (var inFile = File.Open(inFilePath, FileMode.Open, FileAccess.Read))
using (var outFile = File.CreateText(outFilePath))
{
    using (var reader = ExcelReaderFactory.CreateReader(inFile, new ExcelReaderConfiguration() { FallbackEncoding = Encoding.GetEncoding(1252) }))
    using (var writer = new JsonTextWriter(outFile))
    {
        writer.Formatting = Formatting.Indented; //I likes it tidy
        writer.WriteStartArray();
        reader.Read();
        do
        {
            while (reader.Read())
            {
                //peek ahead? Bail before we start anything so we don't get an empty object
                var Name = reader.GetString(0);
                if (string.IsNullOrEmpty(Name)) break;
                
                writer.WriteStartObject();
                workers worker = new workers();
                
                worker.Name = Name;
                worker.Age = Convert.ToInt32(reader.GetDouble(1));
                worker.DepartmentName = reader.GetString(2);
                
                workersList.Add(worker);
                writer.WriteEndObject();
            }
        } while (reader.NextResult());
        writer.WriteEndArray();
        
        
       
    }
        
}

var contextOptions = new DbContextOptionsBuilder<AppContext>()
    .UseSqlServer(@"Server=localhost;Integrated Security=true;Database=ExcelToSQL;TrustServerCertificate=True",
        builder => builder.EnableRetryOnFailure() )
    .Options;

using var context = new AppContext(contextOptions);

context.Workers.AddRange(workersList);
await context.SaveChangesAsync();




