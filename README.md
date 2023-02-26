# RnMapi

Вітаю!

Дана апішка має 2 гілки на випадок якщо я неправильно зрозумів тз. Якщо б у мене була можливість оперативно уточнитись,
в роботі я зробив би саме так. Неточність полягає в тому, що в тз GetPerson має повертати одну людину. 
Втім, якщо звертатись на апі по імені rick чи rick sanchez, вона може повернути декількох персонажів.
Тому я вирішив створити дві гілки. Одна повертає json файл з першим персонажем з колекції, а друга повертає всю колекцію.
У випадку, якщо оріджин невідомий, як у персонажа Unity, в полях оріджину проставляються null. 

Архітектура проекту являє собою multilayer проект, в якому є контролер з двома actions, сервіс, який обробляє дані і форматує респонси, 
сервіс, який відповідає за зв'язок зі сторонньою апі. Моделі, які необхідні для сереалізації і роботи з даними. Під час розробки апі
я керувався принципами dry(наприклад, винесенням логіки отримання респонсу від сторонньої апі в інший клас), kiss(читабельність коду і 
простота коду допомагає краще розібратись в коді і зменшує кількість потенційних помилок) та solid(нажаль, не всюди вдалося успішно реалізувати
ці принципи, як от з IRnMApiCaller, що спробував трішки виправити IApiCaller).

В будь-якому разі буду дуже вдячний за зворотній зв'язок, критику і коментарі. Що можна вважати хорошими і правильними рішеннями, 
що можна було зробити краще.