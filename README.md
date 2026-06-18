# Arrow-Inventory

A web-based IT asset inventory system built with ASP.NET Core Razor Pages, designed for managing devices, sites, and users across a lab or small business environment.

Built as a portfolio and learning project — transitioning from a PowerShell scripting background into full-stack C# web development.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 10 (Razor Pages) |
| Database | SQLite via Entity Framework Core |
| Authentication | ASP.NET Core Identity |
| UI | Bootstrap 5, Material Symbols |
| Hosting | Microsoft Azure (App Service) |
| Source Control | Azure DevOps → GitHub |

---

## Features

### Device Management
- Add, edit, and delete devices from inventory
- Fields include hostname, site, serial number, model, IP, MAC address, CPU, RAM, storage, OS, and description
- Sortable table columns with hostname search
- Row click to view full device details in a modal
- Export inventory to CSV

### Site Management
- Create and manage sites with a site name and site code
- Devices are assigned to a site — site assignment is mandatory
- Sites managed via the Admin panel

### User Management
- Create users with username, display name, email, password, and role
- Roles: Admin, Read & Write, Read Only
- Edit user details and role
- Admin password reset for any user
- Self-service password reset for logged-in users
- Password expiry enforced at 31 days — expired passwords trigger a forced reset on login
- Admins cannot delete their own account

### Authentication
- ASP.NET Core Identity with local username and password
- All pages gated behind authentication — no URL bypassing
- Admin pages restricted to Admin role
- Default admin account seeded on first run with known credentials
- Logout with confirmation prompt

### UI
- Collapsible sidebar navigation with state persistence
- Light and dark theme toggle with localStorage persistence
- Responsive stat cards on dashboard (Total Devices, VMs, Physical)
- Account dropdown in sidebar footer (reset password, logout)
- Consistent card and table styling across all pages

---

## Getting Started

### Prerequisites
- .NET 10 SDK
- Visual Studio 2022 or VS Code

### Running Locally

```bash
git clone https://github.com/ArrowLabsDan/Arrow-Inventory.git
cd Arrow-Inventory
dotnet run
```

On first run the application will:
1. Apply all EF Core migrations automatically
2. Create the SQLite database (`ArrowInventory.db`)
3. Seed a default admin account if no users exist

**Default credentials:**
```
Username: admin
Password: Admin@123!
```

> Change this immediately via Manage Users after first login.

---

## Project Structure

```
Arrow-Inventory/
├── .gitignore
├── ArrowInventory.slnx
└── ArrowInventory/
    ├── Migrations/         # EF Core migrations
    ├── Models/             # Devices, Sites, ApplicationUser
    ├── Pages/              # Razor Pages (cshtml + cs)
    │   └── Shared/         # _Layout, _LayoutSidebar
    ├── Properties/
    ├── Services/           # DeviceService, SiteService
    ├── wwwroot/            # Static assets, CSS
    ├── AppDbContext.cs     # EF Core database context
    ├── Program.cs          # App entry point, service registration
    ├── ArrowInventory.csproj
    ├── appsettings.json
    └── appsettings.Development.json
```

---

## Screenshots
### LogIn
<img width="1705" height="1267" alt="image" src="https://github.com/user-attachments/assets/a9c05bf3-673c-42f6-8d5d-3dc5b90552c3" />

### Home
<img width="1702" height="1274" alt="image" src="https://github.com/user-attachments/assets/87e17126-6f29-4720-994b-780c2b00ae2c" />

### Manage Devices
<img width="1703" height="1276" alt="image" src="https://github.com/user-attachments/assets/ac4ff1d0-ad6e-468f-b86e-8b7f8eb247ac" />

### Admin
<img width="1710" height="1274" alt="image" src="https://github.com/user-attachments/assets/4fd04f74-e8bb-4a5f-83a3-a2d0b278c9bd" />

---

## Roadmap

- [ ] Site-based access control (users restricted to assigned sites)
- [ ] Bulk CSV device import with preview
- [ ] Account settings page (display name, email)
- [ ] Azure AD / Entra ID as a second authentication provider
- [ ] WPF desktop port
- [ ] Logging (app events, login history)
- [ ] Email integration (forgot password, expiry notifications)

---

## Version History

### v0.1.0 — JSON prototype
Initial build using flat JSON file storage. Core device CRUD, Bootstrap UI, theme toggle.

### v0.2.0 — SQLite migration
Replaced JSON storage with SQLite via Entity Framework Core. Added EF migrations and auto-apply on startup.

### v0.3.0 — Sites
Introduced site management. Devices require a site assignment. Sites managed via Admin panel.

### v0.4.0 — Authentication
Added ASP.NET Core Identity. Login, logout, user management, role-based access, password expiry.

---

## Author

Daniel Fletcher — IT Systems / PowerShell background transitioning into full-stack C# development.

This project was built from scratch as a practical learning exercise and portfolio piece. Feedback and suggestions welcome via Issues.
