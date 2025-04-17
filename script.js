function triggerSearch() {
    const query = document.getElementById('searchInput').value;
    if (query.trim() === '') {
        alert('Please enter a search term.');
        return;
    }
    fetch(`https:/quotehubapi.onrender.com/api/quote/search/${query}`, {
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

document.getElementById('searchInput').addEventListener('keypress', function(event) {
    if (event.key === 'Enter') {
        triggerSearch();
    }
});