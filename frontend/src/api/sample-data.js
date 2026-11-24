const names = [
  "Juan Dela Cruz", "Maria Santos", "Pedro Reyes", "Ana Garcia",
  "Carlo Mendoza", "Sofia Ramos", "Miguel Torres", "Angela Flores",
  "Mark Villanueva", "Patricia Cruz", "Daniel Bautista", "Nicole Fernandez",
  "Joshua Rivera", "Camille Lopez", "Gabriel Castillo", "Isabella Aquino",
  "Christian Navarro", "Andrea Morales", "Nathan Perez", "Bianca Gonzales",
  "Adrian Lim", "Jasmine Tan", "Rafael Diaz", "Chloe Martin",
  "Francis Herrera"
];

const cars = [
  ["Toyota Supra MK4", "ABC 1234"],
  ["Honda Civic Type R", "XYZ 5678"],
  ["Nissan GT-R R35", "DEF 9012"],
  ["Mitsubishi Lancer Evo X", "GHI 3456"],
  ["Subaru WRX STI", "JKL 7890"],
  ["Mazda RX-7", "MNO 1122"],
  ["Toyota 86", "PQR 3344"],
  ["Honda Civic RS", "STU 5566"],
  ["Ford Mustang GT", "VWX 7788"],
  ["BMW M3", "YZA 9900"],
  ["Nissan Silvia S15", "BCD 2233"],
  ["Toyota GR Yaris", "EFG 4455"],
  ["Porsche 911", "HIJ 6677"],
  ["Honda S2000", "KLM 8899"],
  ["Mazda MX-5", "NOP 1010"],
  ["Toyota GR86", "QRS 2020"],
  ["Chevrolet Camaro", "TUV 3030"],
  ["Lexus IS350", "WXY 4040"],
  ["Hyundai Genesis Coupe", "ZAB 5050"],
  ["Volkswagen Golf GTI", "CDE 6060"],
  ["Nissan 350Z", "FGH 7070"],
  ["Toyota Camry", "IJK 8080"],
  ["Honda Accord", "LMN 9090"],
  ["BMW M4", "OPQ 1111"],
  ["Ford Focus RS", "RST 2222"]
];

const types = [
  "Competitor", "Exhibitor", "VIP", "Competitor", "Exhibitor",
  "Competitor", "VIP", "Exhibitor", "Competitor", "VIP",
  "Exhibitor", "Competitor", "Competitor", "VIP", "Exhibitor",
  "Competitor", "Exhibitor", "VIP", "Competitor", "Exhibitor",
  "VIP", "Competitor", "Exhibitor", "Competitor", "VIP"
];

const statuses = [
  "Confirmed", "Pending", "CheckedIn", "Confirmed", "Pending",
  "Cancelled", "Confirmed", "CheckedIn", "Pending", "Confirmed",
  "Confirmed", "CheckedIn", "Pending", "Cancelled", "Confirmed",
  "CheckedIn", "Pending", "Confirmed", "Confirmed", "Cancelled",
  "CheckedIn", "Pending", "Confirmed", "CheckedIn", "Pending"
];

const fees = {
  Exhibitor: 3500,
  Competitor: 2500,
  VIP: 5000
};

export const sampleRegistrations = Array.from({ length: 25 }, (_, index) => {
  const registrationType = types[index];
  const registrationFee = fees[registrationType];

  const paymentPattern = index % 4;
  const amountPaid =
    paymentPattern === 0 ? 0 :
    paymentPattern === 1 ? registrationFee / 2 :
    paymentPattern === 2 ? registrationFee :
    Math.round(registrationFee / 3);

  return {
    id: `3f2c9a1e-0000-4000-8000-${String(index + 1).padStart(12, "0")}`,
    registrationNumber: `CS27-${String(index + 1).padStart(4, "0")}`,
    registrationDate: new Date(Date.UTC(2027, 1, 10 + index, 8 + (index % 9), 30)).toISOString(),
    participantName: names[index],
    contactNumber: `0917${String(1234567 + index).slice(-7)}`,
    email: `${names[index].toLowerCase().replaceAll(" ", ".")}@example.com`,
    address: `${index + 10} Rizal Street, Sorsogon City`,
    carMakeModel: cars[index][0],
    plateNumber: cars[index][1],
    registrationType,
    status: statuses[index],
    registrationFee,
    amountPaid,
    paymentStatus:
      amountPaid === 0 ? "Unpaid" :
      amountPaid >= registrationFee ? "Paid" : "Partial"
  };
});

export async function getRegistrations({
  participantName = "",
  type = "",
  status = "",
  page = 1,
  pageSize = 10
} = {}) {
  let filtered = [...sampleRegistrations];

  if (participantName.trim()) {
    const search = participantName.trim().toLowerCase();
    filtered = filtered.filter((registration) =>
      registration.participantName.toLowerCase().includes(search)
    );
  }

  if (type) {
    filtered = filtered.filter(
      (registration) => registration.registrationType === type
    );
  }

  if (status) {
    filtered = filtered.filter(
      (registration) => registration.status === status
    );
  }

  filtered.sort(
    (a, b) => new Date(b.registrationDate) - new Date(a.registrationDate)
  );

  const safePageSize = Math.min(50, Math.max(1, Number(pageSize) || 10));
  const totalCount = filtered.length;
  const totalPages = Math.ceil(totalCount / safePageSize);
  const safePage = Math.min(
    Math.max(1, Number(page) || 1),
    Math.max(1, totalPages)
  );

  const startIndex = (safePage - 1) * safePageSize;

  return {
    items: filtered.slice(startIndex, startIndex + safePageSize),
    page: safePage,
    pageSize: safePageSize,
    totalCount,
    totalPages
  };
}

export async function getSummary() {
  const activeRegistrations = sampleRegistrations.filter(
    (registration) => registration.status !== "Cancelled"
  );

  const totalFees = activeRegistrations.reduce(
    (total, registration) => total + registration.registrationFee, 0
  );

  const totalPayments = activeRegistrations.reduce(
    (total, registration) => total + registration.amountPaid, 0
  );

  return {
    totalRegistrations: sampleRegistrations.length,
    confirmed: sampleRegistrations.filter((r) => r.status === "Confirmed").length,
    checkedIn: sampleRegistrations.filter((r) => r.status === "CheckedIn").length,
    pending: sampleRegistrations.filter((r) => r.status === "Pending").length,
    cancelled: sampleRegistrations.filter((r) => r.status === "Cancelled").length,
    totalFees,
    totalPayments,
    uncollectedPayments: totalFees - totalPayments
  };
}
