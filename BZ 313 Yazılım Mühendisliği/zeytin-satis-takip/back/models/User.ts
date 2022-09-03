
import bcryptjs from "bcryptjs";
import mongoose, { Schema, Document } from 'mongoose';

export interface IUser extends Document{
    
    email: string,
    password: string,
    fullName: string,
    company: string

    comparePassword(candidatePassword : string, cb : Function): string
}

const userSchema = new Schema({

    email: { type: String, unique: true },
    password: String,
    name: String,
    fullName: String,
    company: String

}, { timestamps: true });


/**
 * Password hash middleware.
 */
userSchema.pre<IUser>('save', function save(next) {
    const user = this;
    if (!user.isModified('password')) { return next(); }
    bcryptjs.genSalt(10, (err, salt) => {
        if (err) { return next(err); }

        console.log('USERSCHEMA SAVE')
        console.log(user)

        bcryptjs.hash(user.password, salt, (err, hash) => {
            if (err) { return next(err); }
            user.password = hash;
            next();
        });
    });
});

/**
 * Helper method for validating user's password.
 */
userSchema.methods.comparePassword = function comparePassword(candidatePassword : string, cb: Function) {
    
    bcryptjs.compare(candidatePassword, this.password, (err, isMatch) => {
        cb(err, isMatch);
    });

};

const User = mongoose.models.User || mongoose.model<IUser>('User', userSchema);

export default User;