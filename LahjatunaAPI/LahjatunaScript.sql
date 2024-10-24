-- Create Languages table
CREATE TABLE Languages (
    language_id SERIAL PRIMARY KEY,
    language_name VARCHAR(255) NOT NULL,
    language_code VARCHAR(50) NOT NULL,
    script VARCHAR(100)
);

-- Create Users table
CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    role VARCHAR(50),
    translations_count INT DEFAULT 0,
    feedback_count INT DEFAULT 0
);

-- Create Translation Logs table
CREATE TABLE Translation_Logs (
    translation_log_id SERIAL PRIMARY KEY,
    source_language_id INT REFERENCES Languages(language_id) ON DELETE CASCADE,
    target_language_id INT REFERENCES Languages(language_id) ON DELETE CASCADE,
    user_id INT REFERENCES Users(user_id) ON DELETE CASCADE,
    source_text TEXT NOT NULL,
    target_text TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Feedback table
CREATE TABLE Feedback (
    feedback_id SERIAL PRIMARY KEY,
    translation_log_id INT REFERENCES Translation_Logs(translation_log_id) ON DELETE CASCADE,
    user_id INT REFERENCES Users(user_id) ON DELETE CASCADE,
    rating INT CHECK (rating >= 1 AND rating <= 5),
    comment TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Favorites table
CREATE TABLE Favorites (
    favorite_id SERIAL PRIMARY KEY,
    translation_log_id INT REFERENCES Translation_Logs(translation_log_id) ON DELETE CASCADE,
    user_id INT REFERENCES Users(user_id) ON DELETE CASCADE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


-- Create function to update translations_count in Users table
CREATE OR REPLACE FUNCTION update_translations_count()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE Users
    SET translations_count = translations_count + 1
    WHERE user_id = NEW.user_id;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Create trigger on Translation_Logs table
CREATE TRIGGER trigger_update_translations_count
AFTER INSERT ON Translation_Logs
FOR EACH ROW
EXECUTE FUNCTION update_translations_count();



-- Create function to update feedback_count in Users table
CREATE OR REPLACE FUNCTION update_feedback_count()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE Users
    SET feedback_count = feedback_count + 1
    WHERE user_id = NEW.user_id;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Create trigger on Feedback table
CREATE TRIGGER trigger_update_feedback_count
AFTER INSERT ON Feedback
FOR EACH ROW
EXECUTE FUNCTION update_feedback_count();
