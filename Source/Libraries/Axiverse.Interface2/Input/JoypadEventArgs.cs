﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axiverse.Interface2.Input
{
    public class JoypadEventArgs : EventArgs
    {

    }

    public delegate void JoypadEventHandler(object sender, JoypadEventArgs args);
}
