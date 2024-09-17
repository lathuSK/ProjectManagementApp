using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Project
{
   public int Id { get; set; }
   [MaxLength(250)]
   public required string Name { get; set; }
   public required string Detail { get; set; }
   public required string WebHostedDetails { get; set; }
   public required string DatabaseDetails { get; set; }
   public required string MainDeveloper { get; set; }
   public required string SupportDevelopers { get; set; }
   public required string AzureDevopsDetails { get; set; }
   public required string CodeLocationInVM { get; set; }
   public required string LinkedProjects { get; set; }

}
