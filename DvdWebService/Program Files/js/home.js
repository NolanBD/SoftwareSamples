$(document).ready(function () {
    loadDVDs();
	addDvd();
	updateDvd();
	searchDVDs();
});

// load contacts from REST API service to an HTML table
function loadDVDs() {
    clearDVDTable();
    var contentRows = $('#contentRows');
    
    // retrieve and display existing data using GET request
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44307/dvds',
        success: function(dvdArray) {
            $.each(dvdArray, function(index, dvd){
                //retrieve and store the values
                var title = dvd.title;
                var releaseYear = dvd.releaseYear;
                var director = dvd.director;
				var rating = dvd.rating;
				var id = dvd.id;
                
                // build a table using the retrieved values
                var row = '<tr>';
                    row += '<td>' + title + '</td>';
                    row += '<td>' + releaseYear + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td><button type="button" class="btn btn-info" onclick="showEditForm(' + id + ')">Edit</button></td>';
                    row += '<td><button type="button" class="btn btn-danger" onclick="deleteDVD(' + id + ')">Delete</button></td>';
                
                contentRows.append(row);
            })
        },
        
        // create error function to display API error messages
        error: function() {
            $('#errorMessages')
                .append($('<li>')
                .attr({class: 'list-group-item list-group-item-danger'})
                .text('1 Error calling web service. Please try again later.'));
        }
    }); 
}

function searchDVDs() {
    $('#searchButton').click(function (event) {
		clearDVDTable();
		var contentRows = $('#contentRows');
    
		// retrieve and display existing data using GET request
		$.ajax({
			type: 'GET',
			url: 'https://localhost:44307/dvds/' + $('#searchCategory').val() + '/' + $('#enterSearchTerm').val(),
			success: function(dvdArray) {
				$.each(dvdArray, function(index, dvd){
					//retrieve and store the values
					var title = dvd.title;
					var releaseYear = dvd.releaseYear;
					var director = dvd.director;
					var rating = dvd.rating;
					var id = dvd.id;
                
					// build a table using the retrieved values
					var row = '<tr>';
						row += '<td>' + title + '</td>';
						row += '<td>' + releaseYear + '</td>';
						row += '<td>' + director + '</td>';
						row += '<td>' + rating + '</td>';
						row += '<td><button type="button" class="btn btn-info" onclick="showEditForm(' + id + ')">Edit</button></td>';
						row += '<td><button type="button" class="btn btn-danger" onclick="deleteDVD(' + id + ')">Delete</button></td>';
					
					contentRows.append(row);
				})
			},
        
			// create error function to display API error messages
			error: function() {
				$('#errorMessages')
					.append($('<li>')
					.attr({class: 'list-group-item list-group-item-danger'})
					.text('1 Error calling web service. Please try again later.'));
			}
		});
	})		
}
function showAddForm(){
	$('#dvdTableDiv').hide();
	$('#addFormDiv').show();
}

function hideAddForm() {
	$('#errorMessages').empty();
    
	$('#addTitle').val('');
	$('#addReleaseYear').val('');
	$('#addDirector').val('');
	$('#addRating').val('');
	$('#addNotes').val('');

	$('#dvdTableDiv').show();
	$('#addFormDiv').hide();
	loadDVDs();
}


function addDvd() {
    $('#addButton').click(function (event) {
        $.ajax({
           type: 'POST',
           url: 'https://localhost:44307/dvd',
           data: JSON.stringify({
				title: $('#addTitle').val(),
				releaseYear: $('#addReleaseYear').val(),
				director: $('#addDirector').val(),
				rating: $('#addRating').val(),
				notes: $('#addNotes').val(),
           }),
           headers: {
               'Accept': 'application/json',
               'Content-Type': 'application/json'
           },
           'dataType': 'json',
           success: function() {
				$('#errorMessages').empty();
    
				$('#addTitle').val('');
				$('#addReleaseYear').val('');
				$('#addDirector').val('');
				$('#addRating').val('');
				$('#addNotes').val('');

				$('#dvdTableDiv').show();
				$('#addFormDiv').hide();
				loadDVDs();
           },
           error: function () {
               $('#errorMessages')
                .append($('<li>')
                .attr({class: 'list-group-item list-group-item-danger'})
                .text('Error calling web service. Please try again later.')); 
           }
        })
    });
}

// clear DVD table
function clearDVDTable() {
    $('#contentRows').empty();
}

// open Edit DVD form and load data for selected record
function showEditForm(id) {
    $('#errorMessages').empty();
    
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44307/dvd/' + id,
        success: function(data, status) {
            $('#editTitle').val(data.title);
            $('#editReleaseYear').val(data.releaseYear);
            $('#editDirector').val(data.director);
            $('#editRating').val(data.rating);
            $('#editNotes').val(data.notes);
            $('#editId').val(data.id);
        },
        error: function() {
            $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Error calling web service. Please try again later.')); 
        }
    })
    
    // hide the table when the form is opened
    $('#dvdTableDiv').hide();
    $('#editFormDiv').show();
}

//save the fields to our existing object we want to edit
function updateDvd(id){
	$('#updateButton').click(function(event) {
		$.ajax({
			type: 'PUT',
			url: 'https://localhost:44307/dvd/' + $('#editId').val(),
			data: JSON.stringify({
				title: $('#editTitle').val(),
				releaseYear: $('#editReleaseYear').val(),
				director: $('#editDirector').val(),
				rating: $('#editRating').val(),
				notes: $('#editNotes').val(),
				id: $('#editId').val()
			}),
			headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
			'dataType': 'json',
            'success': function() {
                $('#errorMessage').empty();
                hideEditForm();
                loadDVDs();
            },
			'error': function() {
                $('#errorMessages')
                .append($('<li>')
                .attr({class: 'list-group-item list-group-item-danger'})
                .text('Error calling web service. Please try again later.')); 
            }
		});
	})
}

function deleteDVD(id){
	  $.ajax({
        type: 'DELETE',
        url: 'https://localhost:44307/dvd/' + id,
        success: function() {
            loadDVDs();
        }
    });
}

// when closing the Edit form, empty all fields, hide the form, and show the table
function hideEditForm() {
    $('#errorMessages').empty();
    
    $('#editTitle').val('');
    $('#editReleaseYear').val('');
    $('#editDirector').val('');
    $('#editRating').val('');
    $('#editNotes').val('');

    $('#dvdTableDiv').show();
    $('#editFormDiv').hide();
}
