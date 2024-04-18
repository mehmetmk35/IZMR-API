using IzmirGunesAPI.Application.DTOs.TBLFATUIRS;
using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLFATUIRS;
using IzmirGunesAPI.Persistence.Contexts;
using System.Data;
using System.Data.SqlClient;

namespace IzmirGunesAPI.Persistence.Repositorys
{
    public class TblFatuirsRepository : BaseManager, ITblFatuirsRepository
    {
        public async Task<List<TBLFATUIRS>> GetFatuirs(int Page,int Size,string company)
        {
            #region GetInvoice

            
            List<TBLFATUIRS> List = new();
            String sorgu = String.Format(@"SELECT 
                                                FATIRS_NO,
                                                CARI_KODU,
                                                DBO.TRK(CS.CARI_ISIM) AS CARI_ISIM,
                                                FT.TARIH,
                                                FT.GENELTOPLAM,
                                                FT.FATKALEM_ADEDI
                                                FROM TBLFATUIRS FT
                                                JOIN TBLCASABIT CS ON FT.CARI_KODU=CS.CARI_KOD
                                                order by TARIH DESC 
                                                OFFSET @OFFSET ROWS FETCH NEXT @NEXT ROW ONLY");
            IDbCommand cmd = base.PrepareCommand(sorgu, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;
            
            try
            {
                await conn.OpenAsync();
                base.AddParameterWithValue(cmd, "@OFFSET", Page*Size);
                base.AddParameterWithValue(cmd, "@NEXT", Size);
                SqlDataReader datareader = (SqlDataReader)cmd.ExecuteReader();
                
                while (await datareader.ReadAsync())
                {
                    TBLFATUIRS Invoce = new();
                    Invoce.InvoiceNumber = base.GetSafeString(datareader, 0, "");
                    Invoce.CustomerCode = base.GetSafeString(datareader, 1, "");
                    Invoce.CustomerName = base.GetSafeString(datareader, 2, "");
                    Invoce.Date = base.GetSafeDateTime(datareader, 3, Configuration.CurrentTimeTr);
                    Invoce.TotalAmount = base.GetSafeDecimal(datareader, 4, 0);
                    Invoce.ItemCount = base.GetSafeInt16(datareader, 5, 0);
                    List.Add(Invoce);
                }
            }            
            finally
            {
                base.HandleConnection(conn);
            }
            #endregion
            return List;
        }
    }
}
