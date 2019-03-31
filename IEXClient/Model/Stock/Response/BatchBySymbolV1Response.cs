﻿using IEXClient.Model.Shared.Response;
using System.Collections.Generic;

namespace IEXClient.Model.Stock.Response
{
    public class BatchBySymbolV1Response : BatchBySymbolResponse
    {
        public new List<NewsV1> news { get; set; }
    }
}