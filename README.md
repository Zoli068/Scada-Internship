Realizacija bezbednog Modbus protocol driver-apospecifikaciji M13-TC13-Security-v21 2018-07-24.
Napraviti protocol driver (masterislave) koji de moéi medusobnodarazmene porukepoModbus protokolu. Komunikacija
se odvija kroz TCP stack, koji mote biti bez enkripcijeiautentikacije,alikonfigurabilnodapodr2i TLS1.2+saX509
sertifikatima za mutualnu autentikaciju.
Obezbediti dva nacina uspostave veze: Connect/Disconnect, gde uspostavu konekcije nakon prekida kontrolise aplikacija,i
On/Off Scan, gde uspostavu veze kontrolise protocol driver. Uobaslucaja promenu stanja konekcije protocol driver javlja
kroz event.
Implementirati pun Modbus protokol stack (function code-ovi od 1-24) za obe strane protocoldrivera.Implementirati bazu
modbus tacakai expose-ovati spregu za dodavanje automatizma (slozenog simulatora). 
