﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models
{
    public class OrderDTO
    {
        [Required]
        public string OrderNumber { get; set; }
    }
}