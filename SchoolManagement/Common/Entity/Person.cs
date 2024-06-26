﻿using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Common.Entity;

public class Person
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? ProfilePictureFileName { get; set; }
}
