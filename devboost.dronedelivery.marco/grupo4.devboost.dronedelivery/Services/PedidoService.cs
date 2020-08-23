﻿using Geolocation;
using grupo4.devboost.dronedelivery.Data;
using grupo4.devboost.dronedelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grupo4.devboost.dronedelivery.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IDroneService _droneService;

        public PedidoService(IDroneService droneService)
        {
            _droneService = droneService;
        }

        public grupo4devboostdronedeliveryContext context { get; set; }

        public async Task<DroneDTO> DroneAtendePedido(Pedido pedido)
        {
            double latitudeSaidaDrone = -23.5880684;
            double longitudeSaidaDrone = -46.6564195;

            double distance = GeoCalculator.GetDistance(latitudeSaidaDrone, longitudeSaidaDrone, pedido.Latitude, pedido.Longitude, 1, DistanceUnit.Kilometers) * 2;

            _droneService.GerenciarDrones();

            var drones = await _droneService.GetDisponiveis();

            var buscaDrone = drones.Where(d => d.PerformanceRestante >= distance && d.CapacidadeRestante >= pedido.Peso).FirstOrDefault();

            if (buscaDrone == null)
                return null;

            buscaDrone.PerformanceRestante -= (float)distance;
            buscaDrone.CapacidadeRestante -= pedido.Peso;
            buscaDrone.Situacao = (int)EStatusDrone.AGUARDANDO;

            if ( (buscaDrone.CapacidadeRestante <= (buscaDrone.Capacidade * 0.5)) || (buscaDrone.PerformanceRestante <= (buscaDrone.Perfomance * 0.5)) )
                buscaDrone.Situacao = (int)EStatusDrone.OCUPADO;

            _droneService.Update(buscaDrone);

            return new DroneDTO(buscaDrone, distance);

        }
    }
}
