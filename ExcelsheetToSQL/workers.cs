using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExcelsheetToSQL;

public class workers
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string DepartmentName { get; set; }
}