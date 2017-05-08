using InvoiceBussinesLayer.UseCases;
using InvoiceDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Configuration;
using WcfService1.DataContracts.Input;
using WcfService1.DataContracts.Response;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        InvoiceUseCases _invoiceUseCases = new InvoiceUseCases();

        string app = WebConfigurationManager.AppSettings["Application"];
        string pass = WebConfigurationManager.AppSettings["Password"];
        public CreateInvoiceHeaderResponse SV_300_001_CreateInvoiceHeader(CreateInvoiceHeaderInput input)
        {

            var response = new CreateInvoiceHeaderResponse();
            response.Base = new BaseResponse();
            try
            {
                _invoiceUseCases.ValidateIncomingRequest(app, pass);
                var toCreate = _invoiceUseCases.UC_301_001_CreateInvoiceHeader(input.VATNumber);
                if (!toCreate.IsValid)
                {
                    response.Base.Succes = false;
                    foreach (var item in toCreate.BrokenRulesCollection)
                    {
                        response.Base.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Base.Succes = true;
                    response.Id = toCreate.ID;
                    response.InvoiceNumber = toCreate.InvoiceNumber;
                    response.VATNumber = toCreate.VATNumber;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Base.Succes = false;
                response.Base.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }
        public BaseResponse SV_300_002_AddInvoiceLineToHeader(AddInvoiceLineInput input)
        {
            var response = new BaseResponse();
            try
            {
                _invoiceUseCases.ValidateIncomingRequest(app, pass);
                var toAdd = _invoiceUseCases.UC_301_002_AddInvoiceLineToHeader(new InvoiceBussinesLayer.BusinessModel.CriteriaObjects.AddInvoiceLineToHeaderCriteria() {
                    
                    Description = input.Description,
                    ID = input.InvoiceHeaderId,
                    PricePerUnit = input.PricePerUnit,
                    Quantity = input.Quantity,
                    VATRate = input.VATRate
                });
                if (!toAdd.IsValid)
                {
                    response.Succes = false;
                    foreach(var item in toAdd.BrokenRulesCollection)
                    {
                        response.Errors.Add(new DataContracts.Response.Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new DataContracts.Response.Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }
        public GetInvoiceHeaderResponse SV_300_003_GetInvoiceHeader(GetInvoiceHeaderInput input)
        {
            var response = new GetInvoiceHeaderResponse();
            response.Base = new BaseResponse();
            try
            {
                _invoiceUseCases.ValidateIncomingRequest(app, pass);
                var toFetch = _invoiceUseCases.UC_301_003_GetInvoiceByID(input.InvoiceHeaderId);
                if (toFetch != null && toFetch.ID != 0) {
                    response.Base.Succes = true;
                    response.Id = toFetch.ID;
                    response.Amount = toFetch.Amount;
                    response.InvoiceNumber = toFetch.InvoiceNumber;
                    response.IsPaid = toFetch.IsPaid;
                    response.TotalAmount = toFetch.TotalAmount;
                    response.VATAmount = toFetch.VATAmount;
                    response.VATNumber = toFetch.VATNumber;
                    if(toFetch.InvoiceLines != null && toFetch.InvoiceLines.Count > 0)
                    {
                        foreach(var item in toFetch.InvoiceLines)
                        {
                            response.InvoiceLines.Add(new InvoiceLine()
                            {
                                Amount = item.Amount,
                                Description = item.Description,
                                Id = item.ID,
                                LineAmount = item.LineAmount,
                                PricePerUnit = item.PricePerUnit,
                                Quantity = item.Quantity,
                                VATAmount = item.VATAmount,
                                VATRate = item.VATRate
                            });
                        }
                    }
                    else
                    {
                        response.Base.Succes = false;
                        response.Base.Errors.Add(new Error()
                        {
                            ErrorMessage = "No InvoiceHeader was found"
                        });
                    }
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Base.Succes = false;
                response.Base.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }
    }
}

