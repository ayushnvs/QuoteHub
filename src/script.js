const API_URL = 'https://quotehubapi.onrender.com';

function triggerSearch() {
    const query = document.getElementById('searchInput').value;
    if (query.trim() === '') {
        alert('Please enter a search term.');
        return;
    }
    fetch(`${API_URL}/api/quote/search/${query}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            const quotesContainer = document.getElementById('quotesContainer');
            quotesContainer.innerHTML = ''; // Clear previous results

            if (data.status === 'Success' && data.data.length > 0) {
                // Move the search box to the top
                document.getElementById('searchContainer').classList.add('top');

                // Display quotes and authors
                data.data.forEach(item => {
                    const quoteDetail = document.createElement('div');
                    quoteDetail.className = 'quote-detail';
                    const quoteItem = document.createElement('div');
                    quoteItem.className = 'quote-item';
                    quoteItem.innerHTML = `"${item.quoteText}"`;

                    const quoteAuthor = document.createElement('div');
                    quoteAuthor.className = 'quote-author';
                    quoteAuthor.innerHTML = `- ${item.author.name}`;

                    quoteDetail.appendChild(quoteItem);
                    quoteDetail.appendChild(quoteAuthor);
                    quotesContainer.appendChild(quoteDetail);
                });
            } else {
                // Show no quotes message
                const noQuotesMessage = document.createElement('div');
                noQuotesMessage.className = 'no-quotes';
                noQuotesMessage.innerHTML = 'No quotes found for your search.';
                quotesContainer.appendChild(noQuotesMessage);
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

document.getElementById('searchInput').addEventListener('keypress', function (event) {
    if (event.key === 'Enter') {
        triggerSearch();
    }
});


function openContactDialog() {
    const dialog = document.getElementById('contactDialog');
    const overlay = document.getElementById('contactOverlay');
    dialog.style.display = 'block';
    overlay.style.display = 'block';
}

function closeContactDialog() {
    const dialog = document.getElementById('contactDialog');
    const overlay = document.getElementById('contactOverlay');
    dialog.style.display = 'none';
    overlay.style.display = 'none';
}

function openQuickCreateForm() {
    document.getElementById('quickCreateForm').style.display = 'block';
}

function closeQuickCreateForm() {
    document.getElementById('quickCreateForm').style.display = 'none';
    document.getElementById('addAuthorForm').style.display = 'none';
}

function openAddAuthorForm() {
    document.getElementById('addAuthorForm').style.display = 'block';
    document.querySelector('.sec-search-author').style.display = 'none';
    document.querySelector('.sec-submit-quickcreate').style.display = 'none';
    document.querySelector('.sec-select-language').style.display = 'none';
    document.querySelector('.sec-add-quote').style.display = 'none';
}

function searchAuthor() {
    const query = document.getElementById('authorSearch').value;
    if (query.trim() === '') return;

    fetch(`${API_URL}/api/author/search/${query}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    })
        .then((response) => response.json())
        .then((data) => {
            const dropdown = document.getElementById('authorDropdown');
            dropdown.innerHTML = ''; // Clear previous options

            if (data.status === 'Success' && data.data.length > 0) {
                data.data.forEach((author) => {
                    const option = document.createElement('option');
                    option.value = author.id;
                    option.textContent = author.name;
                    dropdown.appendChild(option);
                });
            } else {
                dropdown.innerHTML = '<option>No authors found</option>';
            }
        })
        .catch((error) => console.error('Error:', error));
}

function addAuthor() {
    const name = document.getElementById('authorName').value;
    const gender = document.getElementById('authorGender').value;
    const alias = document.getElementById('authorAlias').value;
    const dob = document.getElementById('authorDOB').value;

    fetch(`${API_URL}/api/author`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ name, gender, alias, dateOfBirth: dob }),
    })
        .then((response) => response.json())
        .then((data) => {
            alert('Author added successfully!');
            document.getElementById('addAuthorForm').style.display = 'none';
            document.querySelector('.sec-search-author').style.display = 'block';
            document.querySelector('.sec-submit-quickcreate').style.display = 'none';
            document.querySelector('.sec-select-language').style.display = 'none';
            document.querySelector('.sec-add-quote').style.display = 'none';
        })
        .catch((error) => console.error('Error:', error));
}

function submitQuote() {
    const quoteText = document.getElementById('quoteText').value;
    const authorId = document.getElementById('authorDropdown').value;
    const languageId = document.getElementById('languageDropdown').value;

    fetch(`${API_URL}/api/quote`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ quoteText, authorId, languageId }),
    })
        .then((response) => response.json())
        .then((data) => {
            alert('Quote added successfully!');
            closeQuickCreateForm();
        })
        .catch((error) => console.error('Error:', error));
}