import "./newsletter.css"
import newsLetterImg from "../assets/example.jpg"
import newsLetterImg2 from "../assets/example2.jpg"

function Newsletter(){
    return (<div className="newsletter-container container">
    <section className="newsletter-section">
        <p className="section-title">Newsletter</p>
         <img id="newsletter-image" src={newsLetterImg}/>
         <img id="newsletter-image" src={newsLetterImg2}/>  
    </section>
   
    </div>)
}

export default Newsletter;