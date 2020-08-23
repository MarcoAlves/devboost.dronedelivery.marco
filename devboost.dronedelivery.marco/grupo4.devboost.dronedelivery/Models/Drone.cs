﻿using System.ComponentModel.DataAnnotations;

namespace grupo4.devboost.dronedelivery.Models
{
    public class Drone
    {
        [Key]
        public int Id { get; set; }
        public int Capacidade { get; set; }
        public int CapacidadeRestante { get; set; }
        public int Velocidade { get; set; }
        public int Autonomia { get; set; }
        public int Carga { get; set; }
        public int Situacao { get; set; }



        public float Perfomance { get; set; }
        public float PerformanceRestante { get; set; }


    }
}
