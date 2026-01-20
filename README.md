# shorter-url-api
## Minimal API in .NET for URL Shortening
### A complete URL shortening and analytics REST API built with .NET 8 Minimal APIs.
---

##  Overview  
**ShorterAPI** allows users to create, manage, and analyze shortened URLs.  
It supports authentication, usage tracking, favorites, reports, and tagging (coming soon).

---

##  Base URL  
[https://shortys.segurab.com/swagger/index.html](https://shortys.segurab.com/swagger/index.html   )

## API Sections

### Identity  
User authentication and account management.
| Method | Endpoint | Description |
|--------|-----------|-------------|
| POST | `/identity/register` | Register a new user |
| POST | `/identity/login` | Log in with email and password |

---

### 🔗 URL  
Main endpoints for managing short URLs.
| Method | Endpoint | Description |
|--------|-----------|-------------|
| POST | `/url` | Create a new short URL |
| PUT | `/url/{id}` | Update an existing short URL |
| DELETE | `/url/{id}` | Delete a short URL |
| POST | `/url/generate` | Generate a short URL automatically |
| POST | `/url/generate/bulk` | Generate multiple short URLs at once |
| GET  | `/url/{id}/stats` | Get usage stats for a short URL |
| GET  | `/url/check/{shortUrl}` | Check if a short URL already exists |
| GET  | `/url/page/{page}/{pageSize}` | Paginated list of user URLs |

---

### Reports  
Analytics and usage insights.
| Method | Endpoint | Description |
|--------|-----------|-------------|
| GET | `/reports/top-urls/limit/{limit}` | Most used URLs |
| GET | `/reports/least-used/limit/{limit}` | Least used URLs |
| GET | `/reports/last-urls/limit/{limit}` | Most recently created URLs |
| GET | `/reports/random-urls/limit/{limit}` | Random URL samples |
| GET | `/reports/top-urls/limit/{limit}/csv` | Export top URLs to CSV |
| GET | `/reports/last-urls/limit/{limit}/csv` | Export recent URLs to CSV |

---

### Favorites  
Manage user favorite short URLs.
| Method | Endpoint | Description |
|--------|-----------|-------------|
| POST | `/favs` | Add a URL to favorites |
| POST | `/favs/bulk` | Add multiple favorites |
| DELETE | `/favs/{id}` | Remove a favorite |
| DELETE | `/favs/all` | Remove all favorites |
| GET | `/favs/page/{page}/{pageSize}` | Get paginated favorites |

---

### Log Redirect  
Track every redirect and URL visit.
| Method | Endpoint | Description |
|--------|-----------|-------------|
| GET | `/logredirect/{id}` | Retrieve redirect details |
| GET | `/logredirect/page/{page}/{pageSize}` | Paginated redirect logs |

---

## Tech Stack
- **.NET 8 Minimal API**  
- **Entity Framework Core**  
- **MySql**  
- **Swagger / OpenAPI**  
- **JWT Authentication**

---

## License
MIT License © 2025 

---
