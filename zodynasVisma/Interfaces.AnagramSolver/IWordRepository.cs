﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.AnagramSolver
{
    public interface IWordRepository
    {
        string[] ReadFromFile();
    }
}
