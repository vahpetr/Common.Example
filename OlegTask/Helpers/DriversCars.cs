namespace OlegTask.Helpers
{
    /// <summary>
    /// Связующая таблица водителей и машин
    /// </summary>
    public static class DriversCars
    {
        public static int DriverId { get; set; }
        public static int CarId { get; set; }

        /// <summary>
        /// Подключить машину
        /// </summary>
        /// <param name="driverId">Идентификационный  номер водителя</param>
        /// <param name="carId">Идентификационный номер машины</param>
        /// <returns>Запросв</returns>
        public static string Attach(int driverId, int carId)
        {
            return $"INSERT [OlegTask].{typeof (DriversCars).Name} VALUES ({driverId}, {carId})";
        }

        /// <summary>
        /// Отсоединить машину
        /// </summary>
        /// <param name="driverId">Идентификационный  номер водителя</param>
        /// <param name="carId">Идентификационный номер машины</param>
        /// <returns>Запросв</returns>
        public static string Detach(int driverId, int carId)
        {
            return $"DELETE [OlegTask].{typeof (DriversCars).Name} " +
                   $"WHERE {nameof(DriverId)}={driverId} " +
                   $"AND {nameof(CarId)}={carId}";
        }
    }
}