//import moment from "moment";

export const combineDateAndTime = (date: Date, time: Date) => {
    //const year = date.getFullYear();
    //const month = date.getMonth() + 1;
    //const day = date.getDate();
    const dateString = date.toISOString().split('T')[0];
    const timeString = time.toISOString().split('T')[1];

    const dtIso = new Date(dateString + 'T' + timeString);
    return dtIso;
    //console.log(dateString + 'T' + timeString);

    //console.log(moment(date.toISOString()).format('YYYY-MM-DD') + 'T' + moment(time.toISOString()).format('HH:mm:SS') + '.000Z');

    //const dtLocal = new Date(moment(date).format('YYYY-MM-DD') + 'T' + moment(time.toISOString()).format('HH:mm:SS') + '.000Z');

    ////const dtLocal: Date = moment(dtIso).local().toDate();
    //console.log(dtLocal);
    //return dtLocal;
};