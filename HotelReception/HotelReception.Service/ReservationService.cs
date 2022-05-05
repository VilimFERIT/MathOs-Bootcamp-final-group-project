using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Repository.Common;
using HotelReception.Service.Common;

namespace HotelReception.Service
{
    public class ReservationService : IReservationService
    {
        protected IReservationRepository ReservationRepository { get; set; }

        public ReservationService(IReservationRepository repository)
        {
            ReservationRepository = repository;
        }


        public async Task<List<IReservation>> GetReservationsAsync(Sorting sorting, Paging paging, ReservationFiltering filtering)
        { 

            return await ReservationRepository.GetReservationsAsync(sorting, paging, filtering);

        }


        public async Task PostReservationAsync(InsertReservationDomain newReservation)
        {
            // Instantiate objects, distribute properties and send them to repository where the transaction will be executed
            Payment payment = new Payment
            {
                Method = newReservation.PaymentMethod
            };

            Receipt receipt = new Receipt
            {
                Id = newReservation.RecId,
                Price = newReservation.Price,
                //PaymentMethod = newReservation.PaymentMethod,
                ReservationId = newReservation.ReservationId
            };


            Reservation reservation = new Reservation
            {
                Id = newReservation.ResId,
                CreationDate = DateTime.Now,
                IsActive = true
            };

            
            ReceptionistModel receptionist = new ReceptionistModel
            {
                FirstName = newReservation.FirstName,
                LastName = newReservation.LastName,
            };
            

            RoomReservation roomReservation = new RoomReservation
            {
                StartDate = newReservation.StartDate,
                EndDate = newReservation.EndDate
            };

            string pid = newReservation.Pid;

            

            await ReservationRepository.PostReservationAsync(receipt, reservation, receptionist, roomReservation, payment, pid);
        }


        public async Task PutReservationAsync(Guid id)
        {
            await ReservationRepository.PutReservationAsync(id);
        }
    }
}
