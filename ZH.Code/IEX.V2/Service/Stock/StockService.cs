﻿using ZH.Code.IEX.V2.Helper;
using ZH.Code.IEX.V2.Model.Shared.Response;
using ZH.Code.IEX.V2.Model.Stock.Request;
using ZH.Code.IEX.V2.Model.Stock.Response;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZH.Code.IEX.V2.Service.Stock
{
    internal class StockService : IStockService
    {
        private readonly string _pk;
        private readonly Executor _executor;

        public StockService(HttpClient client, string pk)
        {
            _pk = pk;
            _executor = new Executor(client);
        }

        public async Task<BalanceSheetResponse> BalanceSheetAsync(string symbol, Period period, int last = 1)
        {
            const string urlPattern = "stock/[symbol]/balance-sheet/[last]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);
            qsb.Add("period", period.ToString().ToLower());

            var pathNvc = new NameValueCollection
            {
                { "symbol", symbol },
                { "last", last.ToString() }
            };

            return await _executor.ExecuteAsync<BalanceSheetResponse>(urlPattern, pathNvc, qsb);
        }

        public async Task<string> BalanceSheetFieldAsync(string symbol, Period period, string field, int last = 1)
        {
            const string urlPattern = "stock/[symbol]/balance-sheet/[last]/[field]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);
            qsb.Add("period", period.ToString().ToLower());

            var pathNvc = new NameValueCollection {{"symbol", symbol}, {"last", last.ToString()}, {"field", field}};

            return await _executor.ExecuteAsync<string>(urlPattern, pathNvc, qsb);
        }

        public async Task<BatchBySymbolResponse> BatchBySymbolAsync(string symbol, IEnumerable<BatchType> types, string range = "", int last = 1)
        {
            if (types?.Count() < 1)
            {
                throw new ArgumentNullException(nameof(types));
            }

            const string urlPattern = "stock/[symbol]/batch";

            var qsType = new List<string>();
            foreach (var x in types)
            {
                switch (x)
                {
                    case BatchType.Quote:
                        qsType.Add("quote");
                        break;

                    case BatchType.News:
                        qsType.Add("news");
                        break;

                    case BatchType.Chart:
                        qsType.Add("chart");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(types));
                }
            }
            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);
            qsb.Add("types", string.Join(",", qsType));
            if (!string.IsNullOrWhiteSpace(range))
            {
                qsb.Add("range", range);
            }
            qsb.Add("last", last);

            var pathNvc = new NameValueCollection {{"symbol", symbol}};

            return await _executor.ExecuteAsync<BatchBySymbolResponse>(urlPattern, pathNvc, qsb);
        }

        public async Task<Dictionary<string, BatchBySymbolResponse>> BatchByMarketAsync(IEnumerable<string> symbols, IEnumerable<BatchType> types, string range = "", int last = 1)
        {
            if (types?.Count() < 1)
            {
                throw new ArgumentNullException("batchTypes cannot be null");
            }
            else if (symbols?.Count() < 1)
            {
                throw new ArgumentNullException("symbols cannot be null");
            }

            const string urlPattern = "stock/market/batch";

            var qsType = new List<string>();
            foreach (var x in types)
            {
                switch (x)
                {
                    case BatchType.Quote:
                        qsType.Add("quote");
                        break;

                    case BatchType.News:
                        qsType.Add("news");
                        break;

                    case BatchType.Chart:
                        qsType.Add("chart");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(types));
                }
            }
            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);
            qsb.Add("symbols", string.Join(",", symbols));
            qsb.Add("types", string.Join(",", qsType));
            if (!string.IsNullOrWhiteSpace(range))
            {
                qsb.Add("range", range);
            }
            qsb.Add("last", last);

            var pathNvc = new NameValueCollection();

            return await _executor.ExecuteAsync<Dictionary<string, BatchBySymbolResponse>>(urlPattern, pathNvc, qsb);
        }

        public async Task<BookResponse> BookAsync(string symbol)
        {
            const string urlPattern = "stock/[symbol]/book";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"symbol", symbol}};

            return await _executor.ExecuteAsync<BookResponse>(urlPattern, pathNvc, qsb);
        }

        public async Task<CashFlowResponse> CashFlowAsync(string symbol, Period period, int last = 1)
        {
            const string urlPattern = "stock/[symbol]/cash-flow/[last]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);
            qsb.Add("period", period.ToString().ToLower());

            var pathNvc = new NameValueCollection {{"symbol", symbol}, {"last", last.ToString()}};

            return await _executor.ExecuteAsync<CashFlowResponse>(urlPattern, pathNvc, qsb);
        }

        public async Task<string> CashFlowFieldAsync(string symbol, Period period, string field, int last = 1)
        {
            const string urlPattern = "stock/[symbol]/cash-flow/[last]/[field]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);
            qsb.Add("period", period.ToString().ToLower());

            var pathNvc = new NameValueCollection {{"symbol", symbol}, {"last", last.ToString()}, {"field", field}};

            return await _executor.ExecuteAsync<string>(urlPattern, pathNvc, qsb);
        }

        public async Task<IEnumerable<Quote>> CollectionsAsync(CollectionType collection, string collectionName)
        {
            const string urlPattern = "stock/market/collection/[collectionType]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"collectionType", collection.ToString().ToLower()}};

            return await _executor.ExecuteAsync<IEnumerable<Quote>>(urlPattern, pathNvc, qsb);
        }

        public async Task<CompanyResponse> CompanyAsync(string symbol)
        {
            const string urlPattern = "stock/[symbol]/company";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"symbol", symbol}};

            return await _executor.ExecuteAsync<CompanyResponse>(urlPattern, pathNvc, qsb);
        }

        public async Task<DelayedQuoteResponse> DelayedQuoteAsync(string symbol)
        {
            const string urlPattern = "stock/[symbol]/delayed-quote";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"symbol", symbol}};

            return await _executor.ExecuteAsync<DelayedQuoteResponse>(urlPattern, pathNvc, qsb);
        }

        public async Task<IEnumerable<DividendResponse>> DividendAsync(string symbol, DividendRange range)
        {
            const string urlPattern = "stock/[symbol]/dividends/[range]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection
            {
                {"symbol", symbol}, {"range", range.ToString().ToLower().Replace("_", "")}
            };

            return await _executor.ExecuteAsync<IEnumerable<DividendResponse>>(urlPattern, pathNvc, qsb);
        }

        public async Task<EarningResponse> EarningAsync(string symbol, int last = 1)
        {
            const string urlPattern = "stock/[symbol]/earnings/[last]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"symbol", symbol}, {"last", last.ToString()}};

            return await _executor.ExecuteAsync<EarningResponse>(urlPattern, pathNvc, qsb);
        }

        public async Task<string> EarningFieldAsync(string symbol, string field, int last = 1)
        {
            const string urlPattern = "stock/[symbol]/earnings/[last]/[field]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"symbol", symbol}, {"last", last.ToString()}, {"field", field}};

            return await _executor.ExecuteAsync<string>(urlPattern, pathNvc, qsb);
        }

        public async Task<Dictionary<string, EarningTodayResponse>> EarningTodayAsync()
        {
            const string urlPattern = "stock/market/today-earnings";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection();

            return await _executor.ExecuteAsync<Dictionary<string, EarningTodayResponse>>(urlPattern, pathNvc, qsb);
        }

        public async Task<IEnumerable<EffectiveSpreadResponse>> EffectiveSpreadAsync(string symbol)
        {
            const string urlPattern = "stock/[symbol]/effective-spread";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"symbol", symbol}};

            return await _executor.ExecuteAsync<IEnumerable<EffectiveSpreadResponse>>(urlPattern, pathNvc, qsb);
        }

        public async Task<EstimateResponse> EstimateAsync(string symbol, int last = 1)
        {
            const string urlPattern = "stock/[symbol]/estimates/[last]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"symbol", symbol}, {"last", last.ToString()}};

            return await _executor.ExecuteAsync<EstimateResponse>(urlPattern, pathNvc, qsb);
        }

        public async Task<string> EstimateFieldAsync(string symbol, string field, int last = 1)
        {
            const string urlPattern = "stock/[symbol]/estimates/[last]/[field]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"symbol", symbol}, {"last", last.ToString()}, {"field", field}};

            return await _executor.ExecuteAsync<string>(urlPattern, pathNvc, qsb);
        }

        public async Task<FinancialResponse> FinancialAsync(string symbol, int last = 1)
        {
            const string urlPattern = "stock/[symbol]/financials/[last]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"symbol", symbol}, {"last", last.ToString()}};

            return await _executor.ExecuteAsync<FinancialResponse>(urlPattern, pathNvc, qsb);
        }

        public async Task<string> FinancialFieldAsync(string symbol, string field, int last = 1)
        {
            const string urlPattern = "stock/[symbol]/financials/[last]/[field]";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"symbol", symbol}, {"last", last.ToString()}, {"field", field}};

            return await _executor.ExecuteAsync<string>(urlPattern, pathNvc, qsb);
        }

        public async Task<FundOwnershipResponse> FundOwnershipAsync(string symbol)
        {
            const string urlPattern = "stock/[symbol]/fund-ownership";

            var qsb = new QueryStringBuilder();
            qsb.Add("token", _pk);

            var pathNvc = new NameValueCollection {{"symbol", symbol}};

            return await _executor.ExecuteAsync<FundOwnershipResponse>(urlPattern, pathNvc, qsb);
        }
    }
}