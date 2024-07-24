
    document.getElementById('registrationForm').addEventListener('submit', function(event) {
            const firstName = document.getElementById('firstName').value.trim();
    const lastName = document.getElementById('lastName').value.trim();
    const email = document.getElementById('email').value.trim();
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;
    const phone = document.getElementById('phone').value.trim();
    const address = document.getElementById('address').value.trim();

    let isValid = true;

    // Clear previous error messages
    document.getElementById('firstNameError').innerText = '';
    document.getElementById('lastNameError').innerText = '';
    document.getElementById('emailError').innerText = '';
    document.getElementById('passwordError').innerText = '';
    document.getElementById('confirmPasswordError').innerText = '';
    document.getElementById('phoneError').innerText = '';
    document.getElementById('addressError').innerText = '';

    // Validate required fields
    if (!firstName) {
        document.getElementById('firstNameError').innerText = 'First name is required.';
    isValid = false;
            }
    if (!lastName) {
        document.getElementById('lastNameError').innerText = 'Last name is required.';
    isValid = false;
            }
    if (!email) {
        document.getElementById('emailError').innerText = 'Email address is required.';
    isValid = false;
            }
    if (!password) {
        document.getElementById('passwordError').innerText = 'Password is required.';
    isValid = false;
            }
    if (!confirmPassword) {
        document.getElementById('confirmPasswordError').innerText = 'Confirm password is required.';
    isValid = false;
            }
    if (!phone) {
        document.getElementById('phoneError').innerText = 'Phone number is required.';
    isValid = false;
            }
    if (!address) {
        document.getElementById('addressError').innerText = 'Address is required.';
    isValid = false;
            }

    // Validate password
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    if (password && !passwordRegex.test(password)) {
        document.getElementById('passwordError').innerText = 'Password must be at least 8 characters long and contain at least one number, one special character, and one uppercase letter.';
    isValid = false;
            }

    // Check if passwords match
    if (password !== confirmPassword) {
        document.getElementById('confirmPasswordError').innerText = 'Passwords do not match.';
    isValid = false;
            }

    // Prevent form submission if any validation fails
    if (!isValid) {
        event.preventDefault();
            }
        });
