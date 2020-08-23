using Dapper;
using grupo4.devboost.dronedelivery.Data;
using grupo4.devboost.dronedelivery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace grupo4.devboost.dronedelivery.Services
{
    public class DroneService : IDroneService
    {
        private readonly grupo4devboostdronedeliveryContext _context;

        public DroneService(grupo4devboostdronedeliveryContext context)
        {
            _context = context;
        }

        public Drone GetById(int id)
        {
            return _context.Drone.FirstOrDefault(_ => _.Id == id);
        }

        public async Task<List<Drone>> GetAll()
        {
            return _context.Drone.ToList();
        }

        public async Task<List<Drone>> GetDisponiveis()
        {
            return _context.Drone.Where(_ => _.Situacao != 2).ToList();
        }

        public void Update(Drone drone)
        {
            _context.Entry(drone).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
        }

        public void GerenciarDrones()
        {
            #region SQL - OBTER DRONES JÁ RETORNARAM
            string sqlCommand = @"
                    SELECT 
                    a.DroneId,
                    0 as Situacao,
                    a.Id as PedidoId,
                    a.DataHoraFinalizacao
                    FROM Pedido a
                    INNER JOIN Drone ON Drone.Id = a.DroneId
                    WHERE Drone.Situacao = 2";
            #endregion

            using SqlConnection conexao = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; Initial Catalog=desafio-drone");

            var drones = conexao.Query<StatusDroneDTO>(sqlCommand).ToList();
            var dronesAgrupados = drones.GroupBy(_ => new { _.DroneId });

            foreach (var item in dronesAgrupados)
            {
                var dataFinalizacao = item.Max(_ => _.DataHoraFinalizacao);

                if(dataFinalizacao < DateTime.Now)
                {
                    var droneTemp = GetById(item.Key.DroneId);
                    droneTemp.PerformanceRestante = droneTemp.Perfomance;
                    droneTemp.CapacidadeRestante = droneTemp.Capacidade;
                    droneTemp.Situacao = (int)EStatusDrone.DISPONIVEL;

                    Update(droneTemp);
                }
            }

        }

    }
}
