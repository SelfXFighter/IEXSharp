﻿using System.Collections.Generic;
using ZH.Code.IEX.V2.Model.Shared.Response;

namespace ZH.Code.IEX.V2.Model.Stock.Response
{
    public class CashFlowResponse
    {
        public string symbol { get; set; }
        public List<Cashflow> cashflow { get; set; }
    }
}