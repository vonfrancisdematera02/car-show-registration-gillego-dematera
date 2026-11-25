import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || "http://localhost:5205",
  headers: {
    "Content-Type": "application/json"
  }
});

export async function getRegistrations({
  participantName = "",
  type = "",
  status = "",
  page = 1,
  pageSize = 10
} = {}) {
  const params = {
    page,
    pageSize
  };

  if (participantName.trim()) {
    params.participantName = participantName.trim();
  }

  if (type) {
    params.registrationType = type;
  }

  if (status) {
    params.status = status;
  }

  const response = await api.get("/api/registrations", { params });

  return response.data;
}

export async function getSummary() {
  const response = await api.get("/api/registrations/summary");
  const data = response.data;

  return {
    totalRegistrations: data.totalRegistrations,
    confirmed: data.confirmedRegistrations,
    checkedIn: data.checkedInRegistrations,
    pending: data.pendingRegistrations,
    cancelled: data.cancelledRegistrations,
    totalFees: data.totalRegistrationFees,
    totalPayments: data.totalAmountPaid,
    uncollectedPayments: data.outstandingBalance
  };
}
