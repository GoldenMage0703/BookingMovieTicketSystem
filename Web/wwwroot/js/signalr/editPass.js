document.getElementById('changePassForm').addEventListener('submit', function (event) {
    const previousPassword = document.getElementById('previousPassword').value;
    const currentPassword = document.getElementById('currentPassword').value;
    const newPassword = document.getElementById('newPassword').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    let valid = true;

    // Reset error messages
    document.getElementById('currentPasswordError').style.display = 'none';
    document.getElementById('confirmPasswordError').style.display = 'none';
    document.getElementById('passwordError').style.display = 'none';

    // Check if current password is correct
    if (currentPassword !== previousPassword) {
        document.getElementById('currentPasswordError').style.display = 'block';
        valid = false;
    }

    // Check if new password matches confirm password
    if (newPassword !== confirmPassword) {
        document.getElementById('confirmPasswordError').style.display = 'block';
        valid = false;
    }

    // Validate new password against requirements
    const passwordPattern = /^(?=.*\d)(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    if (!passwordPattern.test(newPassword)) {
        document.getElementById('passwordError').style.display = 'block';
        valid = false;
    }

    if (!valid) {
        event.preventDefault(); // Prevent form submission
    }
});
