# Datalagring
Course-Submission

# Relationer
EmployeeEntity:

En EmployeeEntity kan ha flera CommentEntity.
En CommentEntity tillhör en EmployeeEntity.

###CustomerEntity:

En CustomerEntity kan ha flera CaseEntity.
En CustomerEntity har en AddressEntity.

CommentEntity:

En CommentEntity tillhör en CaseEntity.
En CommentEntity skapas av en EmployeeEntity.

CaseStatusEntity:

En CaseStatusEntity kan vara kopplad till flera CaseEntity.

CaseEntity:

En CaseEntity har en CustomerEntity.
En CaseEntity har en CaseStatusEntity.
En CaseEntity kan ha flera CommentEntity.

AddressEntity:

En AddressEntity kan ha flera CustomerEntity.
En CustomerEntity har en AddressEntity.
