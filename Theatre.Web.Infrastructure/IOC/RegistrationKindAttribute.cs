﻿using System;

 namespace Theatre.Web.Infrastructure.IOC
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegistrationKindAttribute : Attribute
    {
        public RegistrationType Type { get; set; }
        public bool AsSelf { get; set; }
    }
}