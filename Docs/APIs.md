# QuoteHub API Documentation

This document provides an overview of the APIs available in the `QuoteController` for managing quotes.

---

## **Endpoints**

### 1. Add a Quote
- **Endpoint**: `POST /api/quote`
- **Description**: Adds a new quote to the database.
- **Request Body**: 
  - **Response**: Returns the added quote.

---

### 2. Get a Quote by ID
- **Endpoint**: `GET /api/quote/{quoteId}`
- **Description**: Retrieves a quote by its unique ID.
- **Response**: Returns the quote if found.

---

### 3. Get All Quotes
- **Endpoint**: `GET /api/quote`
- **Description**: Retrieves all quotes.
- **Response**: Returns a list of quotes.

---

### 4. Update a Quote
- **Endpoint**: `PUT /api/quote/{quoteId}`
- **Description**: Updates an existing quote.
- **Request Body**: 
  - **Response**: Confirms the update.

---

### 5. Delete a Quote
- **Endpoint**: `DELETE /api/quote/{quoteId}`
- **Description**: Deletes a quote by its ID.
- **Response**: Confirms the deletion.

---

### 6. Get Quotes by Author ID
- **Endpoint**: `GET /api/quote/author/{authorId}`
- **Description**: Retrieves quotes by a specific author's ID.
- **Response**: Returns a list of quotes.

---

### 7. Get Quotes by Language ID
- **Endpoint**: `GET /api/quote/language/{languageId}`
- **Description**: Retrieves quotes by a specific language's ID.
- **Response**: Returns a list of quotes.

---

### 8. Get Quotes by Language Name
- **Endpoint**: `GET /api/quote/language/name/{languageName}`
- **Description**: Retrieves quotes by a specific language's name.
- **Response**: Returns a list of quotes.

---

### 9. Get Quotes by Author Name
- **Endpoint**: `GET /api/quote/author/name/{authorName}`
- **Description**: Retrieves quotes by a specific author's name.
- **Response**: Returns a list of quotes.

---

### 10. Get Quotes by Author Alias
- **Endpoint**: `GET /api/quote/author/alias/{authorAlias}`
- **Description**: Retrieves quotes by a specific author's alias.
- **Response**: Returns a list of quotes.

---

### 11. Search Quotes by Partial String
- **Endpoint**: `GET /api/quote/search/{partialString}`
- **Description**: Searches for quotes containing a specific partial string.
- **Response**: Returns a list of matching quotes.

---

## **Future Enhancements**
- **Pagination and Filtering**: Add support for paginated and filtered results.
- **Sorting**: Add support for sorting quotes by fields like `dateCreated`, `authorName`, etc.
- **Bulk Operations**: Add endpoints for bulk creation, update, or deletion of quotes.
- **Advanced Search**: Add full-text search capabilities.

---

This document provides a comprehensive overview of the existing APIs. For further details, refer to the source code.
