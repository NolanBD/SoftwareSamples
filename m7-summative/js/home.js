$(document).ready(function () {
    loadItems();
	addDollar();
	addQuarter();
	addDime();
	addNickle();
	makePurchase();
	makeChange();
});

// load items from REST API service to an HTML table
function loadItems() {
    var itemGrid = $('#itemButtonContainer');
    
    // retrieve and display existing data using GET request
    $.ajax({
        type: 'GET',
        url: 'http://tsg-vending.herokuapp.com/items',
        success: function(itemArray) {
            $.each(itemArray, function(index, item){
                //retrieve and store the values
                var name = item.name;
                var price = item.price;
				var quantity = item.quantity
                var itemId = item.id;
                
                //build a button using the retrieved values
                var item = '<button type="button" class="m-3 btn btn-primary col-md-4" id="itemNumber' + itemId + '" onclick="addItem(' + itemId + ')">'
					item += '<br/><br/><div class="col-md-12">#' + itemId + '</div><br/><br/>';
					item += '<div class="col-md-12">' + name + '</div>';
					item += '<div class="col-md-12">$' + price.toFixed(2) + '</div><br/><br/>';
					item += '<div class="col-md-12">Quantity Left: ' + quantity + '</div><br/><br/><br/><br/>';
					item += '</button>';
				
				//add button to the item grid
                itemGrid.append(item);
            });
        },
        
        // create error function to display API error messages
        error: function() {
			$('#messagesOut').empty();
            $('#messagesOut').text('Error calling web service. Please try again later.');
        }
    }); 
}

//do the maths for adding money used to make purchases
function addMoney(addValue) {	
	var oldValue = parseFloat($('#moneyIn').text());
	var newValue = oldValue + addValue;
	
	$('#moneyIn').empty();
	$('#moneyIn').text(newValue.toFixed(2));
}

function addDollar() {
	$('#addDollar').click(function (event) {
		addMoney(1.00);
	});
}

function addQuarter() {
	$('#addQuarter').click(function (event) {
		addMoney(0.25);
	});
}

function addDime() {
	$('#addDime').click(function (event) {
		addMoney(0.10);
	});
}

function addNickle() {
	$('#addNickle').click(function (event) {
		addMoney(0.05);
	});
}

function addItem(itemId) {
	$('#itemSelectionOut').text(itemId);
	$('#changeOut').empty();
}

function makePurchase() {
	$('#makePurchase').click(function (event) {
		$('#messagesOut').empty();
		//format data to be usable in the URL
		var selectedItem = $('#itemSelectionOut').text();
		var totalMoneyIn = $('#moneyIn').text();
		
		//if item is selected on the outset, send an error to the user
		if (selectedItem == '') {
			$('#messagesOut').text('Please Select An Item!');
			return false;
		};
		
		$.ajax({
			type: 'POST',
			//insert the user selected cash ammount and selected item into the url
			url: 'http://tsg-vending.herokuapp.com/money/' + totalMoneyIn + '/item/' + selectedItem,
			success: function(changeArray) {
				//call the method to tabulate the remainder of the money the user has submitted and then refresh the
				//item button grid to make sure the inventory has been refreshed for the user
				calculateChange(changeArray);
				$('#messagesOut').text('THANK YOU!!!');
				$('#itemButtonContainer').empty();
				loadItems();
			},
			
			//if any of the error conditions handled by the API are met (error code 422) then have the message box
			//display that error
			error: function(data) {
				$('#messagesOut').empty();
				if (data.status === 422){
					$('#messagesOut')
					.text(data.responseJSON.message);
				//if the error isn't handled by the API then throw a generic error
				} else {
					$('#messagesOut').empty();
					$('#messagesOut')
                .text('Error calling web service. Please try again later.');
				}
			}
		});
	});
}

//intake the returned change from the makePurchase function and update our total money remaining
function calculateChange(changeArray) {
	//parse the JSON string into an int that can work with the add$ functions
	$('#moneyIn').empty();
	
	for (i = 0; i < parseInt(changeArray.quarters); ++i) {
		addMoney(0.25);;
	}
	
	for (i = 0; i < parseInt(changeArray.dimes); ++i) {
		addMoney(0.10);;
	}
	
	for (i = 0; i < parseInt(changeArray.nickles); ++i) {
		addMoney(0.05);;
	}
	
	for (i = 0; i < parseInt(changeArray.pennies); ++i) {
		addMoney(0.01);;
	}
}

function makeChange() {
  $('#makeChange').click(function(event) {
    //get the numeric value of #moneyIn by removing an non-numeric characters
    var change = $('#moneyIn').text();
    var quarters = 0;
    var dimes = 0;
    var nickles = 0;
    var pennies = 0;

    //if we had exact change for our transaction set all fields to blank
    if (change === 0) {
      $('#changeOut').text('');
      $('#itemSelectionOut').empty();
      $('#messagesOut').empty();
      $('#moneyIn').empty();
      return false;
    };

	//multiply the change by 100 inorder to make the maths easier to deal with
    if (change > 0) {
		change = parseFloat(change) * 100;
	  
		quarters = Math.floor(change / 25);
        change = change % 25;

        dimes = Math.floor(change / 10);
        change = change % 10;

        nickles = Math.floor(change / 5);
        change = change % 5;

        pennies = Math.floor(change);

        coins = quarters + ' Quarters, ' + dimes + ' Dimes, ' + nickles + ' Nickels, ' + pennies + ' Pennies';
    };

    $('#changeOut').text(coins);

    $('#itemSelectionOut').empty();
    $('#messagesOut').empty();
    $('#moneyIn').text('0.00');
  });
}