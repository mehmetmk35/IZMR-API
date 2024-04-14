using IzmirGunesAPI.Application.DTOs;
using IzmirGunesAPI.Application.DTOs.TBLFATUIRS;
using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLSTHAR;
using IzmirGunesAPI.Persistence.Contexts;
using System.Data;
using System.Data.SqlClient;

namespace IzmirGunesAPI.Persistence.Repositorys
{
    internal class DetailTblSthar : BaseManager, IDetailTblSthar
    {
        public async Task<List<TBLSTHAR>> GetDetailInvoice(string Fisno)
        {
            List<TBLSTHAR> List = new();
            String sorgu = String.Format(@"SELECT
                                            STHAR_GCMIK,
                                            STHAR_GCKOD,STHAR_TARIH,
                                            STHAR_NF,
                                            STHAR_BF,
                                            DEPO_KODU
                                            FROM TBLSTHAR
                                            WHERE FISNO=@FISNO
                                              order by STHAR_TARIH DESC 
                                           ");
            IDbCommand cmd = base.PrepareCommand(sorgu, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, String.Empty);
            SqlConnection conn = (SqlConnection)cmd.Connection;

            try
            {
                await conn.OpenAsync();
               
                base.AddParameterWithValue(cmd, "@FISNO", Fisno);
                SqlDataReader datareader = (SqlDataReader)cmd.ExecuteReader();

                while (await datareader.ReadAsync())
                {
                    TBLSTHAR StockMovement = new();
                    StockMovement.StockMovementQuantity = base.GetSafeDecimal(datareader, 0, 0);
                    StockMovement.StockIn_Out = base.GetSafeString(datareader, 1, "");
                    StockMovement.Date = base.GetSafeDateTime(datareader, 2, DateTime.Now);
                    StockMovement.ProductNetPrice = base.GetSafeDecimal(datareader, 3, 0);
                    StockMovement.ProductGrossPrice = base.GetSafeDecimal(datareader, 4, 0);
                    StockMovement.WarehouseCode = base.GetSafeInt16(datareader, 5, 0);
                     
                    List.Add(StockMovement);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                base.HandleConnection(conn);
            }
 
            return List;
        }
    }
}
