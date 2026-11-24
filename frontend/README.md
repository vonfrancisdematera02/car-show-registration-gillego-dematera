# AutoVista Events Car Show 2027 – Registration Dashboard

A React dashboard for managing car show registrations at Sorsogon City Coliseum.

## Features
- Registration dashboard with KPI cards
- Participant name search
- Registration type and status filters
- Debounced search
- Pagination
- Loading, empty, and error states
- Responsive design
- Backend API integration

## Technologies
- React
- Vite
- JavaScript
- Bootstrap
- Axios
- ASP.NET Core Web API

## Installation

Install dependencies:

    npm install

Run the frontend:

    npm run dev

Build the application:

    npm run build

## Backend API

Default API URL: http://localhost:5205

Endpoints:
- GET /api/registrations
- GET /api/registrations/{id}
- GET /api/registrations/summary

Start the backend from the project root:

    dotnet run --project ./Api

## Data Sources

Sample data is stored in src/api/sample-data.js.

Actual API requests are handled by src/api/registrationsApi.js.

To switch data sources, update the imports in src/pages/RegistrationsPage.jsx.

## Folder Structure

    src/
    ├── api/
    ├── components/
    ├── hooks/
    ├── pages/
    ├── App.jsx
    ├── App.css
    ├── index.css
    └── main.jsx
