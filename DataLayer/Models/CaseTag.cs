﻿using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class CaseTag
{
    public int CaseTagId { get; set; }

    public string Name { get; set; } = null!;
}
