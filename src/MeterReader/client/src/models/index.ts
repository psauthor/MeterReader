export interface Address {
	id: number;
	address1: string;
	address2?: any;
	address3?: any;
	cityTown: string;
	stateProvince: string;
	postalCode: string;
	country?: any;
}

export interface Reading {
	id: number;
	customerId: number;
	value: number;
	readingDate: string;
}

export interface Customer {
	id: number;
	name: string;
	companyName?: any;
	phoneNumber: string;
	address: Address;
	readings: Reading[];
}