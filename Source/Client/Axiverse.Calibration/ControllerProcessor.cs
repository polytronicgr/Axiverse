﻿using Axiverse.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axiverse.Calibration
{
    public class ControllerProcessor : Processor<ControllerComponent>
    {
        public override ProcessorStage Stage => ProcessorStage.Preprocessing;
    }
}
