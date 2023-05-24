function esCorreo(correo) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;


    return regex.test(correo);
}
function esCedula(ced) {
    const regex = /^[1-9](?:\d{8}|\d{11})$/



    return regex.test(ced);
}


function esContrasena(ced) {
    const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$/



    return regex.test(ced);
}