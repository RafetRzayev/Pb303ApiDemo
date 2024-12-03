﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allup.Domain.AppSettingsModels
{
    public class Cloudinary
    {
        public required string CloudName {  get; set; }
        public required string Key {  get; set; }
        public required string Secret { get; set; }
    }
}
