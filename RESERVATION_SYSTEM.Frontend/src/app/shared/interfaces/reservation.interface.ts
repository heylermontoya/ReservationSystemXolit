export interface Reservation{
    id:string;
    customerID: string;
    customerName: string;
    serviceID: string;
    serviceName: string;
    dateReservation: string | Date;
    startDate: string | Date;
    endDate: string | Date;
    state: string;
    numberPeople: string;
    total: string;
}
