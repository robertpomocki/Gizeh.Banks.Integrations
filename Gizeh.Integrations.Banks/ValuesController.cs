using Gizeh.Integrations.Banks.IngBankService;
using System.Collections.Generic;
using System.Web.Http;

namespace Gizeh.Integrations.Banks
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            IngBankService.cdcws00101PortClient service = new IngBankService.cdcws00101PortClient("cdc.001.01SOAP");

            IngBankService.GetAccountBalanceRequestType reqParams = new IngBankService.GetAccountBalanceRequestType();

            var h = new CcsHeaderType();
            h.FinInstID = "ING Bank Śląski";
            h.PartnerID = "Firma XYZ";
            h.PassID = "Wirtual User 1";
            h.ProdID = new ProductIdType();
            h.ProdID.langVer = "EN";
            h.ProdID.manufacturer = "Eviware";
            h.ProdID.Value = "soapui";
            h.ProdID.ver = "3.6.1";
            h.Revision = "1";
            h.Version = "1";

            reqParams.Document = new IngBankService.Document();
            reqParams.Document.GetAcct = new IngBankService.GetAccountV04();
            reqParams.Document.GetAcct.AcctQryDef = new IngBankService.AccountQueryDefinition4();
            reqParams.Document.GetAcct.MsgId = new IngBankService.MessageIdentification();
            reqParams.Document.GetAcct.MsgId.Id = "OLEK9";
            reqParams.Document.GetAcct.AcctQryDef.AcctCrit = new
            IngBankService.AccountCriteriaDefinition4Choice();
            IngBankService.AccountCriteria4 accCrit = new IngBankService.AccountCriteria4();
            accCrit.SchCrit = new IngBankService.CashAccountSearchCriteria4[1];
            accCrit.SchCrit[0] = new IngBankService.CashAccountSearchCriteria4();
            accCrit.SchCrit[0].AcctId = new IngBankService.AccountIdentificationSearchCriteriaChoice[1];
            accCrit.SchCrit[0].AcctId[0] = new IngBankService.AccountIdentificationSearchCriteriaChoice();
            accCrit.SchCrit[0].AcctId[0].ItemElementName = new IngBankService.ItemChoiceType1();
            accCrit.SchCrit[0].AcctId[0].ItemElementName = IngBankService.ItemChoiceType1.EQ;
            accCrit.SchCrit[0].AcctId[0].Item = new IngBankService.AccountIdentification1Choice();
            ((IngBankService.AccountIdentification1Choice)accCrit.SchCrit[0].AcctId[0].Item).ItemElementName
            = IngBankService.ItemChoiceType.IBAN;
            ((IngBankService.AccountIdentification1Choice)accCrit.SchCrit[0].AcctId[0].Item).Item =
            "PL05105015331000002203562026";
            accCrit.SchCrit[0].Bal = new IngBankService.BalanceDetails4[1];
            accCrit.SchCrit[0].Bal[0] = new IngBankService.BalanceDetails4();
            accCrit.SchCrit[0].Bal[0].BalTp = new IngBankService.BalanceType3Choice[1];

            accCrit.SchCrit[0].Bal[0].BalTp[0] = new IngBankService.BalanceType3Choice();
            accCrit.SchCrit[0].Bal[0].BalTp[0].Item = IngBankService.BalanceType10Code.AVLB;
            accCrit.SchCrit[0].Bal[0].CtrPtyTp = IngBankService.BalanceCounterparty1Code.MULT;
            reqParams.Document.GetAcct.AcctQryDef.AcctCrit.Item = accCrit;
            IngBankService.GetAccountBalanceRequest req = new IngBankService.GetAccountBalanceRequest(h, reqParams);

            var resp = service.GetAccountBalance(h, reqParams);
            return reqParams.Document.GetAcct.MsgId.Id;

        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
