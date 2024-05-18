function saveCustomer(firstName, lastName) {
    alert(`Customer ${firstName} ${lastName} has been successfully added!`);
}


function saveDog(firstName) {
    alert(`Dog ${firstName} has been successfully added!`);
}

function getReferralSource() {
    var referralSources = ['Old client', 'Door to door flyers', 'Front door sign',
        'Car signs', 'Phone books', 'Facebook', 'Kijiji site', 'Auction', 'Business card',
        'Poster', 'Restaurant menus', 'Trade fairs', 'Garage sales', 'Newspaper', 'Google website'];

    return referralSources;
}

function getCanadianCities() {
    var canadianCities = ['Beaumont', 'Bruderheim', 'Bon Accord', 'Namao', 'Devon', 'Edmonton', 'Elk Island National Park', 'For Saskatchewan', 'Gibbons', 'Joseph Burg', 'Lamoureux', 'Leduc', 'Morinville', 'Redwater', 'Sherwood Park', 'Spruce Grove', 'St Albert', 'Stony Plain', 'Thorsby', 'Tofield', 'Vegreville', 'Other'];
    return canadianCities;
}

function getSex() {
    var sexes = ['Male', 'Female', 'Other'];
    return sexes;
}

function getYesOrNo() {
    var decisions = ['Yes', 'No',];
    return decisions;
}


function getProvinces(city) {
    var cityToProvinceMap = {
        'Beaumont': 'AB',
        'Devon': 'AB',
        'Edmonton': 'AB',
        'Elk Island National Park': 'AB',
        'For Saskatchewan': 'AB',
        'Gibbons': 'AB',
        'Leduc': 'AB',
        'Morinville': 'AB',
        'Redwater': 'AB',
        'Spruce Grove': 'AB',
        'St Albert': 'AB'
    };

    return cityToProvinceMap[city] || 'Unknown'; // Return the province or 'Unknown' if city not found
}

function showErrorMessage(message) {
    // You can customize this function based on your UI library or requirements
    alert("Error: " + message);
}
