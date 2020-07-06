export const combineDateAndTime = (date: Date, time: Date) => {
    //const year = date.getFullYear();
    //const month = date.getMonth() + 1;
    //const day = date.getDate();
    const dateString = date.toISOString().split('T')[0];
    const timeString = time.toISOString().split('T')[1];
        
    return new Date(dateString + 'T' + timeString);
};