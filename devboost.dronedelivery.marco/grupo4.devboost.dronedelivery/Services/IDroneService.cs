using grupo4.devboost.dronedelivery.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grupo4.devboost.dronedelivery.Services
{
    public interface IDroneService
    {
        Task<List<Drone>> GetAll();
        Task<List<Drone>> GetDisponiveis();


        Drone GetById(int id);
        
        
        void Update(Drone drone);

        void GerenciarDrones();

    }
}
