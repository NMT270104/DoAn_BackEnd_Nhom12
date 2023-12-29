INSERT INTO Authors (AuthorName)
VALUES

    ('Harper Lee'),
	('Gabriel Garcia Marquez'),
    ('F. Scott Fitzgerald'),
    ('George Orwell'),
    ('Jane Austen'),
    ('J.D. Salinger '),
	('J.R.R. Tolkien'),
	('Dan Brown'),
	('Paulo Coelho'),
	('Yuval Noah Harari')



INSERT INTO Categories (CategoryName)
VALUES
    ('Truyện tiểu thuyết'),
    ('Tiểu thuyết hiện thực phép thuật'),
    ('Tiểu thuyết'),
    ('Tiểu thuyết chính trị'),
    ('Tiểu thuyết lãng mạn'),
	('Tiểu thuyết hài hước'),
	('Tiểu thuyết phiêu lưu'),
	('Tiểu thuyết trinh thám'),
	('Tiểu thuyết phiêu lưu 2'),
	('Sách lịch sử khoa học')

INSERT INTO Books (NameBook, AuthorID, CategoryID,Description,Image, Price, Quantity)
VALUES
    ('To Kill a Mockingbird', 1, 1, 'Một cô gái tên Scout Finch nhớ lại những sự kiện của tuổi thơ trong một thị trấn nhỏ ở Alabama, Mỹ, nơi bố cô là một luật sư bảo vệ một người đàn ông da đen bị buộc tội một tội án mà ông ta không phạm.','Pictures', 112200, 100),
	('One Hundred Years of Solitude', 2, 2, 'Cuốn tiểu thuyết phép thuật này kể về gia đình Buendía qua bảy thế hệ trong thị trấn Macondo, với sự kết hợp giữa thực tế và huyền bí, mang đến một hành trình lịch sử đầy mê hoặc.','Pictures', 223300, 200),
	('The Great Gatsby', 3, 3, 'Một câu chuyện về tình yêu và sự thất bại, xoay quanh Jay Gatsby, người giàu có nhưng hoang dã, và tình yêu không đáp ứng của anh dành cho Daisy Buchanan, người đã kết hôn với một người khác.','Pictures', 335500, 300),
	('1984', 4, 4, 'Trong xã hội kiểm soát chặt chẽ của quốc gia Oceania, nhân vật chính Winston Smith bắt đầu nhận ra sự giả mạo và kiểm soát từ chính phủ và sự tồn tại của Big Brother.','Pictures', 42200, 420),
	('Pride and Prejudice', 5, 5, 'Elizabeth Bennet, một phụ nữ thông minh và nhanh nhẹn, phải đối mặt với thách thức và những thay đổi trong cuộc sống của mình khi cô gặp gỡ và yêu đương với Mr. Darcy, một người đàn ông giàu có nhưng khó hiểu.','Pictures', 52200, 123),
	('The Catcher in the Rye', 6, 6, 'Holden Caulfield, một học sinh bị trục xuất, đi lang thang ở New York, thể hiện sự lo lắng và sự phân biệt xã hội, đồng thời tìm kiếm ý nghĩa trong cuộc sống.','Pictures', 621300, 235),
	('The Hobbit', 7, 7, 'Bilbo Baggins, một người hobbit nhỏ bé, tham gia một cuộc phiêu lưu huyền bí để giành lại một kho báu khỏi sự kiểm soát của con rồng Smaug, và trải qua những thử thách đầy nguy hiểm.','Pictures', 65540, 516),
	('The Da Vinci Code', 8, 8, 'Giáo sư Robert Langdon và Sophie Neveu phải giải mã một loạt các bí mật lịch sử và tôn giáo để giải cứu một ước mơ cổ xưa và ngăn chặn một âm mưu lớn.','Pictures', 321500, 215),
	('The Alchemist', 9, 9, 'Santiago, một người chăn cừu, rời bỏ quê hương để theo đuổi ước mơ của mình và trải qua một cuộc hành trình tìm kiếm ý nghĩa cuộc sống và ý thức về sự tự do.','Pictures', 132500, 213),
	('Sapiens: A Brief History of Humankind', 10, 10, 'Sách tóm tắt lịch sử của loài người từ thời tiền sử đến hiện đại, đưa ra những quan điểm sâu sắc về cách con người đã và đang tác động lên thế giới xung quanh.','Pictures',130000 ,521 )





select * from AspNetRoles
select * from AspNetUserRoles
select * from AspNetUsers
