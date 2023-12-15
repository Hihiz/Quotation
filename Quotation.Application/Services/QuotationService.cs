using Quotation.Application.Interfaces;
using Quotation.Application.Models;
using Quotation.Domain.Entities;
using Quotation.Domain.Enums;
using System.Xml;

namespace Quotation.Application.Services
{
    public class QuotationService : IQuotationService
    {
        public async Task<IBaseStatus<List<Valute>>> GetQuotationsAsync(DateOnly date)
        {
            List<Valute> valuteList = new List<Valute>();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync($"http://www.cbr.ru/scripts/XML_daily.asp?date_req={date}"))
                {
                    XmlDocument xmlDocument = new XmlDocument();

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    xmlDocument.Load($"http://www.cbr.ru/scripts/XML_daily.asp?date_req={date}");

                    foreach (XmlNode node in xmlDocument.SelectNodes("ValCurs/Valute"))
                    {
                        valuteList.Add(new Valute()
                        {
                            Id = node.Attributes["ID"].Value,
                            NumCode = node["NumCode"]?.InnerText,
                            CharCode = node["CharCode"]?.InnerText,
                            Nominal = node["Nominal"]?.InnerText,
                            Name = node["Name"]?.InnerText,
                            Value = node["Value"]?.InnerText,
                            VunitRate = node["VunitRate"]?.InnerText
                        });
                    }

                    if (valuteList.Count == 0)
                    {
                        return new StatusResponseError<List<Valute>>()
                        {
                            StatusCode = (int)ErrorCode.NotFound,
                            Message = ErrorCode.NotFound.ToString(),
                        };
                    }
                }
            }

            return new StatusResponse<List<Valute>>()
            {
                StatusCode = (int)ErrorCode.OK,
                Data = valuteList
            };
        }
    }
}
